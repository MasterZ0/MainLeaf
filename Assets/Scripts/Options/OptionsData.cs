using System;

namespace AdventureGame.AppOptions
{
    [Serializable]
    public class VideoOptionsData
    {
        public int resolutionWidth;
        public int resolutionHeight;
        public bool fullScreen = true;
    }

    [Serializable]
    public class AudioOptionsData // Serialized = 0 ~ 10 (Slider step values), 0 ~ 1 FMOD
    {
        public float masterVolume = 10f;
        public float musicVolume = 10f;
        public float sfxVolume = 10f;
    }

    [Serializable]
    public class GameplayOptionsData
    {
        public bool damagePopup = true;
        public bool screenShake = true;
        public bool gamepadVibration = true;
    }

    [Serializable]
    public class InputOptionsData
    {
        public string bindingOverrides;
    }
}