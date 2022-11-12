using FMOD.Studio;

namespace AdventureGame.Audio    
{
    /// <summary>
    /// An instance of the sound. Use SoundReference or SoundData's "PlaySound" method to receive a SoundInstance and keep track of the audio.
    /// This class has all the values related to the currently playing sound.
    /// </summary>
    public class SoundInstance
    {
        private EventInstance eventInstance;

        public string Name 
        { 
            get  
            {
                eventInstance.getDescription(out EventDescription description);
                description.getPath(out string result);
                return result;
            }
        }

        internal FMOD.GUID Guid
        {
            get
            {
                eventInstance.getDescription(out var des);
                des.getID(out FMOD.GUID id);
                return id;
            }
        }

        public SoundInstance(EventInstance eventInstance)
        {
            this.eventInstance = eventInstance;
        }

        public void Start()
        {
            eventInstance.start();
            eventInstance.release();
        }

        /// <summary>
        /// The fade will only work if the sound has a fade inside FMOD.
        /// </summary>
        public void StopWithFade() => eventInstance.stop(STOP_MODE.ALLOWFADEOUT);
        public void StopImmediate() => eventInstance.stop(STOP_MODE.IMMEDIATE);
        public void Pause() => eventInstance.setPaused(true);
        public void Unpause() => eventInstance.setPaused(false);
        public bool SoundFinished()
        {
            PLAYBACK_STATE state;
            eventInstance.getPlaybackState(out state);
            return state == PLAYBACK_STATE.PLAYING ? false : true;
        }

        public static implicit operator bool(SoundInstance instance)
        {
            return instance != null && instance.Name != null;
        }
    }
}