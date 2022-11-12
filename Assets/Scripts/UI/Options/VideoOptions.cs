using AdventureGame.Persistence;
using AdventureGame.Shared.ExtensionMethods;
using AdventureGame.UIElements;
using I2.Loc;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame.UI.AppOptions
{
    public class VideoOptions : MonoBehaviour
    {
        [Header("Texts")]
        [SerializeField] private LocalizedString fullscreenOn;
        [SerializeField] private LocalizedString fullscreenOff;
        [Space]
        [SerializeField] private LocalizedString qualityLow;
        [SerializeField] private LocalizedString qualityMedium;
        [SerializeField] private LocalizedString qualityHigh;

        [Header("Components")]
        [SerializeField] private Navigator resolutionNavigator;
        [SerializeField] private Navigator graphicsQualityNavigator;
        [SerializeField] private Navigator fullScreenNavigator;

        private List<Resolution> resolutions = new List<Resolution>();
        private VideoOptionsData videoData;

        private readonly Vector2 AspectRatioRange = new Vector2(16f / 9f - 0.01f, 16f / 9f + 0.01f);
        private const int MinResolution = 240;

        private string[] FullscreenOptions => new string[] { fullscreenOff, fullscreenOn };
        private string[] GraphicQualityOptions => new string[] { qualityLow, qualityMedium, qualityHigh };

        private void Awake()
        {
            LocalizationManager.OnLocalizeEvent += UpdateFullScreenTexts;
        }

        private void OnDestroy()
        {
            LocalizationManager.OnLocalizeEvent -= UpdateFullScreenTexts;
        }

        private void OnDisable()
        {
            PersistenceManager.SaveGlobalFile(videoData);
        }

        private void UpdateFullScreenTexts()
        {
            fullScreenNavigator.UpdateTexts(FullscreenOptions);
            graphicsQualityNavigator.UpdateTexts(GraphicQualityOptions);
        }

        public void LoadVideoSettings()
        {
            videoData = PersistenceManager.LoadGlobalFile<VideoOptionsData>();

            if (videoData == null)
            {
                videoData = new VideoOptionsData();
                videoData.resolutionWidth = Screen.currentResolution.width;
                videoData.resolutionHeight = Screen.currentResolution.height;
                videoData.graphicsQuality = QualitySettings.GetQualityLevel();

                PersistenceManager.SaveGlobalFile(videoData);
            }

            fullScreenNavigator.Init(FullscreenOptions, videoData.fullScreen ? 1 : 0);
            graphicsQualityNavigator.Init(GraphicQualityOptions, videoData.graphicsQuality);
            SetupResolutions();

            Screen.SetResolution(videoData.resolutionWidth, videoData.resolutionHeight, videoData.fullScreen);
            QualitySettings.SetQualityLevel(videoData.graphicsQuality);
        }

        private void SetupResolutions()
        {
            Resolution[] aux = Screen.resolutions;
            List<string> resolutionsOptions = new List<string>();

            // Create a list of machine resolutions
            for (int i = 0; i < aux.Length; i++)
            {
                if (aux[i].width < MinResolution || aux[i].height < MinResolution) // Ignore very low resolution
                    continue;

                float aspect = aux[i].width / (float)aux[i].height;
                if (!AspectRatioRange.InsideRange(aspect))      //Ignore bad resolutions
                    continue;

                string option = aux[i].width + " x " + aux[i].height;
                if (resolutionsOptions.Contains(option))        // Dont Repeat
                    continue;

                resolutions.Add(aux[i]);
                resolutionsOptions.Add(option);
            }

            int resolutionsIndex = -1;  // Find current resolution
            for (int i = 0; i < resolutions.Count; i++)
            {
                if (resolutions[i].width == videoData.resolutionWidth && resolutions[i].height == videoData.resolutionHeight)
                {
                    resolutionsIndex = i;
                    break;
                }
            }

            if (resolutionsIndex == -1) // Set as the highest resolution
            {
                float oldWidth = videoData.resolutionWidth;
                float oldHeight = videoData.resolutionHeight;

                resolutionsIndex = resolutions.Count - 1;
                videoData.resolutionWidth = resolutions[resolutionsIndex].width;
                videoData.resolutionHeight = resolutions[resolutionsIndex].height;

                Debug.LogError($"Resolution not allowed (w:{oldWidth}, h:{oldHeight})\nResolution changing to (w:{videoData.resolutionWidth}, h:{videoData.resolutionHeight})");
            }

            resolutionNavigator.Init(resolutionsOptions.ToArray(), resolutionsIndex);
        }

        #region Set Option
        public void OnSetFullscreen(int isFullscreen)
        {
            bool fullScreen = isFullscreen == 1 ? true : false;
            Screen.fullScreen = fullScreen;
            videoData.fullScreen = fullScreen;
        }

        public void OnSetResolution(int resolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            videoData.resolutionWidth = resolution.width;
            videoData.resolutionHeight = resolution.height;
        }

        public void OnSetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
            videoData.graphicsQuality = qualityIndex;
        }
        #endregion
    }
}