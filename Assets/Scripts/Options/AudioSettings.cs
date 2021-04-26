using FMOD.Studio;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour {
    [Header("Audio Settings")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider voiceSlider;

    [Header(" - Texts")]
    [SerializeField] private TextMeshProUGUI masterTMP;
    [SerializeField] private TextMeshProUGUI musicTMP;
    [SerializeField] private TextMeshProUGUI sfxTMP;
    [SerializeField] private TextMeshProUGUI voiceTMP;

    [Header(" - Audio")]
    [EventRef]
    [SerializeField] private string sfxTest;
    [EventRef]
    [SerializeField] private string voiceTest;

    private static Bus masterBus, musicBus, sfxBus, voiceBus;
    private EventInstance sfxTestEvent;
    private EventInstance voiceTestEvent;

    public static void Setup() {
        masterBus = RuntimeManager.GetBus("bus:/Master");
        musicBus = RuntimeManager.GetBus("bus:/Master/Music");
        sfxBus = RuntimeManager.GetBus("bus:/Master/SFX");
        voiceBus = RuntimeManager.GetBus("bus:/Master/Voice");

        float volume = PlayerPrefs.GetFloat(Constants.PlayerPrefs.Float.MASTER_VOLUME, 1f);
        masterBus.setVolume(volume);

        volume = PlayerPrefs.GetFloat(Constants.PlayerPrefs.Float.MUSIC_VOLUME, .8f);
        musicBus.setVolume(volume);

        volume = PlayerPrefs.GetFloat(Constants.PlayerPrefs.Float.SFX_VOLUME, .8f);
        sfxBus.setVolume(volume);

        volume = PlayerPrefs.GetFloat(Constants.PlayerPrefs.Float.VOICE_VOLUME, .8f);
        voiceBus.setVolume(volume);
    }

    private void Awake() {
        masterBus.getVolume(out float volume);
        masterTMP.text = $"{volume * 100}%";
        masterSlider.value = volume;

        musicBus.getVolume(out volume);
        musicTMP.text = $"{volume * 100}%";
        musicSlider.value = volume;

        sfxBus.getVolume(out volume);
        sfxTMP.text = $"{volume * 100}%";
        sfxSlider.value = volume;

        voiceBus.getVolume(out volume);
        voiceTMP.text = $"{volume * 100}%";
        voiceSlider.value = volume;

        sfxTestEvent = RuntimeManager.CreateInstance(sfxTest);
        voiceTestEvent = RuntimeManager.CreateInstance(voiceTest);
    }
    public void OnSetMasterVolume(float volume) {
        volume = Mathf.Round(volume * 100) / 100;
        masterBus.setVolume(volume);
        masterTMP.text = $"{volume * 100}%";
        PlayerPrefs.SetFloat(Constants.PlayerPrefs.Float.MASTER_VOLUME, volume);
    }
    public void OnSetMusicVolume(float volume) {
        volume = Mathf.Round(volume * 100) / 100;
        musicBus.setVolume(volume);
        musicTMP.text = $"{volume * 100}%";
        PlayerPrefs.SetFloat(Constants.PlayerPrefs.Float.MUSIC_VOLUME, volume);
    }
    public void OnSetSFXVolume(float volume) {
        volume = Mathf.Round(volume * 100) / 100;
        sfxBus.setVolume(volume);        
        sfxTMP.text = $"{volume * 100}%";
        PlayerPrefs.SetFloat(Constants.PlayerPrefs.Float.SFX_VOLUME, volume);

        PLAYBACK_STATE pbState;
        sfxTestEvent.getPlaybackState(out pbState);
        if(pbState != PLAYBACK_STATE.PLAYING) {
            sfxTestEvent.start();
        }
    }
    public void OnSetVoiceVolume(float volume) {
        volume = Mathf.Round(volume * 100) / 100;
        voiceBus.setVolume(volume);
        voiceTMP.text = $"{volume * 100}%";
        PlayerPrefs.SetFloat(Constants.PlayerPrefs.Float.VOICE_VOLUME, volume);

        PLAYBACK_STATE pbState;
        voiceTestEvent.getPlaybackState(out pbState);
        if (pbState != PLAYBACK_STATE.PLAYING) {
            voiceTestEvent.start();
        }
    }    
}
