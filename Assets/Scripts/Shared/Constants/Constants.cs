using UnityEngine;

public static class Constants 
{
    public static class PlayerPrefs {
        public static class Float {
            public static string MUSIC_VOLUME { get => "MusicVolume"; }
            public static string SFX_VOLUME { get => "SfxVolume"; }
            public static string VOICE_VOLUME { get => "VoiceVolume"; }
        }

        public static class Int {
            public static string SELECTED_CHARACTER { get => "SelectedCharacter"; }
        }
    }
    public static class Path {
        public static string HIT { get => "Materials/Hit"; }
        public static string PERSISTENT_SCENE { get => "Assets/Scenes/Static/GameManager.unity"; }
    }
    public static class Anim {
        public static string FADE_IN { get => "FadeIn"; }
        public static string FADE_OUT { get => "FadeOut"; }
        public static string START { get => "Start"; }
        public static string COUNT { get => "Count"; }

        public static string MOVE_SPEED { get => "MoveSpeed"; }
        public static string VELOCITY_X { get => "VelocityX"; }
        public static string VELOCITY_Y { get => "VelocityY"; }
        public static string VELOCITY_Z { get => "VelocityZ"; }
        public static string IS_AIMING { get => "IsAiming"; }
        public static string IS_MOVING { get => "IsMoving"; }
        public static string IS_GROUNDED { get => "IsGrounded"; }
        public static string FIRE { get => "Fire"; }
        public static string ATTACK { get => "Attack"; }
        public static string JUMP { get => "Jump"; }
        public static string IS_ARMED { get => "IsArmed"; }
        public static string DEATH { get => "Death"; }
        public static string SELECT { get => "Select"; }
        public static string DESELECT { get => "Deselect"; }
    }
    public static class Tag {
        public static string PLAYER { get => "Player"; }
        public static string ENEMY { get => "Enemy"; }
    }
    public static class Layer {
        public static LayerMask PLAYER { get => 1; }
        public static LayerMask ENEMY { get => 2; }
        public static LayerMask INVINCIBLE { get => 6; }
    }
}
