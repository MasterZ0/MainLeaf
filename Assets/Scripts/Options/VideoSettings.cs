using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VideoSettings : MonoBehaviour {
    [Header("VideoSettings")]
    [SerializeField] private string fullscreenOn;
    [SerializeField] private string fullscreenOff;
    [SerializeField] private string[] graficsOptions;

    [Header(" - Config")]
    [SerializeField] private Navigator fullScreenNavigator;
    [SerializeField] private Navigator graphicsNavigator;
    [SerializeField] private Navigator resolutionNavigator;

    private Resolution[] resolutions;

    private void Awake() {
        int fullscreenIndex = Screen.fullScreen ? 1 : 0;
        string[] fullscreenOptions = new string[2] { fullscreenOff, fullscreenOn };

        int graficsIndex = QualitySettings.GetQualityLevel();

        //Get the resolutions of the machine
        resolutions = Screen.resolutions;              
        List<string> resolutionsOptions = new List<string>();

        int resolutionsIndex = 0;
        for (int i = 0; i < resolutions.Length; i++) {  // Create a list of machine options
            string option = resolutions[i].width + " x " + resolutions[i].height;
            if (resolutionsOptions.Contains(option))    // Dont Repeat
                continue;

            resolutionsOptions.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                resolutionsIndex = i;
        }


        fullScreenNavigator.Init(fullscreenOptions, fullscreenIndex);
        graphicsNavigator.Init(graficsOptions, graficsIndex);
        resolutionNavigator.Init(resolutionsOptions.ToArray(), resolutionsIndex);
    }

    public void OnSetFullscreen(int isFullscreen) {
        Screen.fullScreen = isFullscreen == 1 ? true : false;
    }

    public void OnSetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void OnSetResolution(int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
