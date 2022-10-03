using FMODUnity;
using FMOD.Studio;
using UnityEngine;
using AdventureGame.Shared;
using System;

namespace AdventureGame.Audio
{
    public enum SoundGroup
    {
        Master,
        Music,
        SFX
    }

    public enum UISound
    {
        Submit,
        Cancel,
        Select
    }

    /// <summary>
    /// Manage the sound requests and returns a SoundInstance.
    /// </summary>
    public class AudioManager : Singleton<AudioManager>
    {
        [Header("UI")]
        [SerializeField] private SoundReference submit;
        [SerializeField] private SoundReference cancel;
        [SerializeField] private SoundReference select;

        private static SoundInstance currentMusic;

        public static void SetCurrentMusic(SoundReference music)
        {
            if (currentMusic != music)
            {
                if (currentMusic)
                {
                    currentMusic.StopWithFade();
                    currentMusic = null;
                }

                if (music)
                    currentMusic = music.PlaySound();
            }
        }

        public static void PlayUISound(UISound soundType)
        {
            SoundReference sound = soundType switch
            {
                UISound.Submit => Instance.submit,
                UISound.Cancel => Instance.cancel,
                UISound.Select => Instance.select,
                _ => throw new NotImplementedException(),
            };

            sound.PlaySound();
        }

        /// <summary>
        /// Set the VCA mixer
        /// </summary>
        /// <param name="volume">0 to 1</param>
        public static void SetVolume(SoundGroup soundGroup, float volume)
        {
            string vcaName = soundGroup.ToString();
            VCA vca = RuntimeManager.GetVCA("vca:/" + vcaName);
            vca.setVolume(volume);
        }

        internal static SoundInstance PlaySound(EventReference eventReference, Transform transform)
        {
            EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
            if (transform != null)
                RuntimeManager.AttachInstanceToGameObject(eventInstance, transform);
            //else
            //eventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(new Vector3()));

            SoundInstance soundInstance = new SoundInstance(eventInstance);
            soundInstance.Start();
            return soundInstance;
        }

        internal static SoundInstance PlaySound(EventReference eventReference, Vector2 position)
        {
            EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
            eventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(position));
            SoundInstance soundInstance = new SoundInstance(eventInstance);
            soundInstance.Start();
            return soundInstance;
        }
    }
}