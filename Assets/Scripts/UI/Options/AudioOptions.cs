using AdventureGame.Audio;
using AdventureGame.Persistence;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame.UI.AppOptions
{
    public class AudioOptions : MonoBehaviour
    {
        [Title("Audio")]
        [SerializeField] private Slider masterVolume;
        [SerializeField] private Slider musicVolume;
        [SerializeField] private Slider sfxVolume;
        [SerializeField] private Slider voiceVolume;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI masterVolumeTmp;
        [SerializeField] private TextMeshProUGUI musicVolumeTmp;
        [SerializeField] private TextMeshProUGUI sfxVolumeTmp;
        [SerializeField] private TextMeshProUGUI voiceVolumeTmp;
        [Space]
        [SerializeField] private SoundReference sfxTest;
        [SerializeField] private SoundReference voiceTest;

        private AudioOptionsData audioData;

        private void OnDisable()
        {
            PersistenceManager.SaveGlobalFile(audioData);
        }

        public void LoadSettings()
        {
            audioData = PersistenceManager.LoadGlobalFile<AudioOptionsData>();

            if (audioData == null)
            {
                audioData = new AudioOptionsData();
                PersistenceManager.SaveGlobalFile(audioData);
            }

            // Set sliders
            masterVolume.SetValueWithoutNotify(audioData.masterVolume);
            musicVolume.SetValueWithoutNotify(audioData.musicVolume);
            sfxVolume.SetValueWithoutNotify(audioData.sfxVolume);
            voiceVolume.SetValueWithoutNotify(audioData.voiceVolume);

            // Set volume
            SetVolume(masterVolumeTmp, SoundGroup.Master, audioData.masterVolume);
            SetVolume(musicVolumeTmp, SoundGroup.Music, audioData.musicVolume);
            SetVolume(sfxVolumeTmp, SoundGroup.SFX, audioData.sfxVolume);
            SetVolume(voiceVolumeTmp, SoundGroup.Voice, audioData.voiceVolume);
        }

        #region Set Option
        public void OnSetMasterVolume(float volume)
        {
            audioData.masterVolume = volume;
            SetVolume(masterVolumeTmp, SoundGroup.Master, volume);
        }

        public void OnSetMusicVolume(float volume)
        {
            audioData.musicVolume = volume;
            SetVolume(musicVolumeTmp, SoundGroup.Music, volume);
        }

        public void OnSetSFXVolume(float volume)
        {
            audioData.sfxVolume = volume;
            SetVolume(sfxVolumeTmp, SoundGroup.SFX, volume);
            sfxTest.PlaySound();
        }

        public void OnSetVoiceVolume(float volume)
        {
            audioData.voiceVolume = volume;
            SetVolume(voiceVolumeTmp, SoundGroup.Voice, volume);
            voiceTest.PlaySound();
        }

        /// <param name="volume">0 ~ 10</param>
        private void SetVolume(TextMeshProUGUI display, SoundGroup soundGroup, float volume)
        {
            display.text = $"{volume}0%";
            AudioManager.SetVolume(soundGroup, volume / 10f);
        }
        #endregion
    }
}