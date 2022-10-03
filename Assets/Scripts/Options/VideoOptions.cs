using AdventureGame.Persistence;
using AdventureGame.Shared.ExtensionMethods;
using AdventureGame.UIElements;
using I2.Loc;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame.AppOptions
{
    public class VideoOptions : MonoBehaviour
    {
        [Header("Texts")]
        [SerializeField] private LocalizedString fullscreenOn;
        [SerializeField] private LocalizedString fullscreenOff;

        [Header("Components")]
        [SerializeField] private Navigator resolutionNavigator;
        [SerializeField] private Navigator fullScreenNavigator;

        private List<Resolution> resolutions = new List<Resolution>();
        private VideoOptionsData videoData;

        private readonly Vector2 AspectRatioRange = new Vector2(1.6f, 1.8f);
        private const int MinResolution = 240;

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
            string[] fullscreenOptions = new string[2] { fullscreenOff, fullscreenOn };
            fullScreenNavigator.UpdateTexts(fullscreenOptions);
        }

        public void LoadVideoSettings()
        {
            bool hasData = PersistenceManager.ContainsGlobalFile<VideoOptionsData>();
            if (hasData)
            {
                videoData = PersistenceManager.LoadGlobalFile<VideoOptionsData>();
            }
            else
            {
                videoData = new VideoOptionsData();
                videoData.resolutionWidth = Screen.currentResolution.width;
                videoData.resolutionHeight = Screen.currentResolution.height;

                PersistenceManager.SaveGlobalFile(videoData);
            }

            SetupNavigators();

            Screen.SetResolution(videoData.resolutionWidth, videoData.resolutionHeight, videoData.fullScreen);
        }

        private void SetupNavigators()
        {
            // Fullscreen
            string[] fullscreenOptions = new string[2] { fullscreenOff, fullscreenOn };
            fullScreenNavigator.Init(fullscreenOptions, videoData.fullScreen ? 1 : 0);

            // Resolutions
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
        public void SetFullscreen(int isFullscreen)
        {
            bool fullScreen = isFullscreen == 1 ? true : false;
            Screen.fullScreen = fullScreen;
            videoData.fullScreen = fullScreen;
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            videoData.resolutionWidth = resolution.width;
            videoData.resolutionHeight = resolution.height;
        }
        #endregion
    }
}