using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour {

    [Header("Config Internal")]
    public AudioMixer audioMixer;
    public Slider musicSlider, sfxSlider, voiceSlider;
    public GameObject[] panels;

    public TextMeshProUGUI musicTMP;
    public TextMeshProUGUI soundEffectsTMP;
    public TextMeshProUGUI voiceTMP;


    private Button lastControlBtn;
    private GameObject lastPanel;

    private bool firstSelect;
    public void Init() {
        float volume;
        volume = PlayerPrefs.GetFloat(Constants.PlayerPrefs.Float.MUSIC_VOLUME);
        audioMixer.SetFloat("Music", volume);
        musicSlider.value = volume;
        musicTMP.text = $"{(volume + 80) * 1.25f}%";

        volume = PlayerPrefs.GetFloat(Constants.PlayerPrefs.Float.SFX_VOLUME);
        audioMixer.SetFloat("SoundEffects", volume);
        sfxSlider.value = volume;
        soundEffectsTMP.text = $"{(volume + 80) * 1.25f}%";

        volume = PlayerPrefs.GetFloat(Constants.PlayerPrefs.Float.VOICE_VOLUME);
        audioMixer.SetFloat("Voice", volume);
        voiceSlider.value = volume;
        voiceTMP.text = $"{(volume + 80) * 1.25f}%";
    }

    public void SetMusicVolume(float volume) {
        audioMixer.SetFloat("Music", volume);
        musicTMP.text = $"{(volume + 80) * 1.25f}%";

        PlayerPrefs.SetFloat(Constants.PlayerPrefs.Float.MUSIC_VOLUME, volume);
    }
    public void SetSFXVolume(float volume) {

        audioMixer.SetFloat("SoundEffects", volume);
        //sfxAudioManager.Play("Select");
        soundEffectsTMP.text = $"{(volume + 80) * 1.25f}%";

        PlayerPrefs.SetFloat(Constants.PlayerPrefs.Float.SFX_VOLUME, volume);
    }
    public void SetVoiceVolume(float volume) {

        audioMixer.SetFloat("Voice", volume);
        //voicesAudioManager.Play("VoiceTest");
        voiceTMP.text = $"{(volume + 80) * 1.25f}%";

        PlayerPrefs.SetFloat(Constants.PlayerPrefs.Float.VOICE_VOLUME, volume);
    }

    public void OnSwitchPanel(int index) {
        if (lastPanel == panels[index]) {
            firstSelect = false;
            return;
        }

        if (lastPanel) {
            lastPanel.SetActive(false);
        }
        lastPanel = panels[index];
        panels[index].SetActive(true);

        if (firstSelect) {
            firstSelect = false;
            return;
        }
        //sfxAudioManager.Play("Select");
    }

    public void OnOpenControlsPanel(Button button) {
        firstSelect = true;
        //sfxAudioManager.Play("Submit");

        if (lastControlBtn) {
            lastControlBtn.Select();
        }
        else {
            button.Select();
        }
    }
    public void OnOpenAudioPanel(Slider slider) {
        firstSelect = true;
        slider.Select();
        //sfxAudioManager.Play("Submit");
    }
    public void OnControlsSelectBtn(Button button) {
        lastControlBtn = button;
        if (firstSelect) {
            firstSelect = false;
            return;
        }
        //sfxAudioManager.Play("Select");
    }

    public void OnSelectBtn() {
        if (firstSelect) {
            firstSelect = false;
            return;
        }
        //sfxAudioManager.Play("Select");
    }
    public void OnSubmitBtn(Button button) {
        firstSelect = true;
        //sfxAudioManager.Play("Submit");
        if (button)
            button.Select();
    }
    public void OnCancelBtn(Button button) {
        firstSelect = true;
        //sfxAudioManager.Play("Cancel");
        if (button)
            button.Select();
    }
    public void OnFirstBtn(string sfxName) {
        firstSelect = true;
        //sfxAudioManager.Play(sfxName);
    }
    private void OnEnable() {
        firstSelect = true;
        panels[0].SetActive(true);
    }
    private void OnDisable() {
        lastPanel.SetActive(false);
    }
}

/*
public class VideoSettings : MonoBehaviour {
    [Header("VideoSettings")]
    public List<string> graficsOptions;
    public List<string> fullscreenOptions;

    [Header(" - Config")]
    public AudioManager audioManager;
    public VideoConfig fullScreenVCG;
    public VideoConfig graficVCG;
    public VideoConfig resolutionVCG;
    public GameObject fullscrenBtn;
    public GameObject graficsBtn;
    public GameObject resolutionsBtn;

    private Resolution[] resolutions;
    private List<string> resolutionsOptions;

    private int fullscreenIndex;
    private int graficsIndex;
    private int resolutionsIndex;

    private Controls controls;

    private void Awake() {
        controls = new Controls();
        controls.UI.Move.performed += ctx => ChangeOption(ctx.ReadValue<Vector2>().x);

        resolutions = Screen.resolutions; //Pega as resoluções do maquina
        resolutionsOptions = new List<string>();

        string oldResolution = string.Empty;
        for (int i = 0; i < resolutions.Length; i++) // Criar uma lista com as opções da maquina
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            if (option == oldResolution)
                continue;

            resolutionsOptions.Add(option);
            oldResolution = option;

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                resolutionsIndex = i;
        }

        if (Screen.fullScreen)
            fullscreenIndex = 1;

        graficsIndex = QualitySettings.GetQualityLevel();

        fullScreenVCG.Init(fullscreenOptions[fullscreenIndex], fullscreenIndex == 0, fullscreenIndex == fullscreenOptions.Count - 1);
        graficVCG.Init(graficsOptions[graficsIndex], graficsIndex == 0, graficsIndex == graficsOptions.Count - 1);
        resolutionVCG.Init(resolutionsOptions[resolutionsIndex], resolutionsIndex == 0, resolutionsIndex == resolutionsOptions.Count - 1);
    }

    private void OnEnable() {
        fullScreenVCG.Load();
        graficVCG.Load();
        resolutionVCG.Load();
    }
    public void OnSubmitVideoBtn() => controls.Enable();
    public void OnExitVideo() => controls.Disable();
    public void ChangeOption(float dir) {
        if (dir == 0)
            return;

        if (EventSystem.current.currentSelectedGameObject == fullscrenBtn.gameObject) {
            fullscreenIndex = ChangeIndex(fullscreenOptions, fullscreenIndex, fullScreenVCG, dir > 0);
            SetFullscreen(fullscreenIndex == 1);
        }
        else if (EventSystem.current.currentSelectedGameObject == graficsBtn) {
            graficsIndex = ChangeIndex(graficsOptions, graficsIndex, graficVCG, dir > 0);
            SetQuality(graficsIndex);
        }
        else if (EventSystem.current.currentSelectedGameObject == resolutionsBtn) {
            resolutionsIndex = ChangeIndex(resolutionsOptions, resolutionsIndex, resolutionVCG, dir > 0);
            SetResolution(resolutionsIndex);
        }
    }

    int ChangeIndex(List<string> listCount, int i, VideoConfig videoConfig, bool goRight) {
        if (goRight) {
            i++;
            if (i == listCount.Count)
                return listCount.Count - 1;

            videoConfig.GoRight(listCount[i], i == listCount.Count - 1);
        }
        else {
            i--;
            if (i == -1)
                return 0;

            videoConfig.GoLeft(listCount[i], i == 0);
        }
        audioManager.Play("Select");
        return i;
    }

    void SetFullscreen(bool isFullscreen) => Screen.fullScreen = isFullscreen;
    void SetQuality(int qualityIndex) => QualitySettings.SetQualityLevel(qualityIndex);
    void SetResolution(int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}

public class VideoConfig : MonoBehaviour {
    public Animator leftArrow;
    public Animator rightArrow;
    public TextMeshProUGUI text;
    private bool leftEnd;
    private bool rightEnd;

    public void Init(string newText, bool isLeftEnd, bool isRightEnd) {
        leftEnd = isLeftEnd;
        rightEnd = isRightEnd;
        text.text = newText;
    }

    public void Load() {
        if (leftEnd)
            leftArrow.Play("Out");
        else
            leftArrow.Play("Off");

        if (rightEnd)
            rightArrow.Play("Out");
        else
            rightArrow.Play("Off");
    }

    public void GoLeft(string newText, bool isLeftEnd) {
        leftEnd = isLeftEnd;
        rightEnd = false;
        rightArrow.Play("Off");
        leftArrow.Play("Select");
        leftArrow.SetBool("Out", leftEnd);
        text.text = newText;
    }

    public void GoRight(string newText, bool isRightEnd) {
        rightEnd = isRightEnd;
        leftEnd = false;
        leftArrow.Play("Off");
        rightArrow.Play("Select");
        rightArrow.SetBool("Out", isRightEnd);
        text.text = newText;
    }
}
*/