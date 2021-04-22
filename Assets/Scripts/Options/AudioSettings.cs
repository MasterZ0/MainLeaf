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

    private static Bus master, music, sfx, voice;
    private EventInstance sfxTestEvent;
    private EventInstance voiceTestEvent;

    private void Awake() {

        float volume;
        //volume = PlayerPrefs.GetFloat(Constants.PlayerPrefs.Float.MASTER_VOLUME, 1f);
        master.getVolume(out volume);
        masterTMP.text = $"{volume * 100}%";
        masterSlider.value = volume;

        //volume = PlayerPrefs.GetFloat(Constants.PlayerPrefs.Float.MUSIC_VOLUME, .8f);
        music.getVolume(out volume);
        musicTMP.text = $"{volume * 100}%";
        musicSlider.value = volume;

        //volume = PlayerPrefs.GetFloat(Constants.PlayerPrefs.Float.SFX_VOLUME, .8f);
        sfx.getVolume(out volume);
        sfxTMP.text = $"{volume * 100}%";
        sfxSlider.value = volume;

        //volume = PlayerPrefs.GetFloat(Constants.PlayerPrefs.Float.VOICE_VOLUME, .8f);
        voice.getVolume(out volume);
        voiceTMP.text = $"{volume * 100}%";
        voiceSlider.value = volume;

        sfxTestEvent = RuntimeManager.CreateInstance("event:/SFX/SFXTest");
        voiceTestEvent = RuntimeManager.CreateInstance("event:/SFX/VoiceTest");
    }

    public static void Setup() {
        float volume;

        master = RuntimeManager.GetBus("bus:/Master");
        music = RuntimeManager.GetBus("bus:/Master/Music");
        sfx = RuntimeManager.GetBus("bus:/Master/SFX");
        voice = RuntimeManager.GetBus("bus:/Master/Voice");

        volume = PlayerPrefs.GetFloat(Constants.PlayerPrefs.Float.MASTER_VOLUME, 1f);
        master.setVolume(volume);

        volume = PlayerPrefs.GetFloat(Constants.PlayerPrefs.Float.MUSIC_VOLUME, .8f);
        music.setVolume(volume);

        volume = PlayerPrefs.GetFloat(Constants.PlayerPrefs.Float.SFX_VOLUME, .8f);
        sfx.setVolume(volume);

        volume = PlayerPrefs.GetFloat(Constants.PlayerPrefs.Float.VOICE_VOLUME, .8f);
        voice.setVolume(volume);
    }

    public void OnSetMasterVolume(float volume) {

        volume = Mathf.Round(volume * 100) / 100;
        master.setVolume(volume);
        masterTMP.text = $"{volume * 100}%";
        PlayerPrefs.SetFloat(Constants.PlayerPrefs.Float.MASTER_VOLUME, volume);
    }
    public void OnSetMusicVolume(float volume) {

        volume = Mathf.Round(volume * 100) / 100;
        music.setVolume(volume);
        musicTMP.text = $"{volume * 100}%";
        PlayerPrefs.SetFloat(Constants.PlayerPrefs.Float.MUSIC_VOLUME, volume);
    }
    public void OnSetSFXVolume(float volume) {
        //if (!active)
        //    return;

        volume = Mathf.Round(volume * 100) / 100;
        sfx.setVolume(volume);        
        sfxTMP.text = $"{volume * 100}%";
        PlayerPrefs.SetFloat(Constants.PlayerPrefs.Float.SFX_VOLUME, volume);

        PLAYBACK_STATE pbState;
        sfxTestEvent.getPlaybackState(out pbState);
        if(pbState != PLAYBACK_STATE.PLAYING) {
            sfxTestEvent.start();
        }
    }
    public void OnSetVoiceVolume(float volume) {
        //if (!active)
        //    return;

        volume = Mathf.Round(volume * 100) / 100;
        voice.setVolume(volume);
        voiceTMP.text = $"{volume * 100}%";
        PlayerPrefs.SetFloat(Constants.PlayerPrefs.Float.VOICE_VOLUME, volume);

        PLAYBACK_STATE pbState;
        voiceTestEvent.getPlaybackState(out pbState);
        if (pbState != PLAYBACK_STATE.PLAYING) {
            voiceTestEvent.start();
        }
    }    
}
