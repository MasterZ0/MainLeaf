using UnityEngine;

public static class Constants 
{
    public static class Path {
        public static string GAME_MANEGER { get => "GameManager"; }
        public static string HIT { get => "Materials/Hit"; }
    }
    public static class Anim {
        public static string FADE_IN { get => "FadeIn"; }
        public static string FADE_OUT { get => "FadeOut"; }
        public static string START { get => "Start"; }
        public static string COUNT { get => "Count"; }

        public static string IS_AIMING { get => "IsAiming"; }
        public static string IS_MOVING { get => "IsMoving"; }
        public static string VELOCITY_X { get => "VelocityX"; }
        public static string VELOCITY_Y { get => "VelocityY"; }
        public static string FIRE { get => "Fire"; }
        public static string ATTACK { get => "Attack"; }
    }
    public static class Scene {
        public static string MAIN_MENU { get => "MainMenu"; }
        public static string GAMEPLAY { get => "Gameplay"; }
    }
    public static class Tag {
        public static string PLAYER { get => "Player"; }
        public static string ENEMY { get => "Enemy"; }
    }
    public static class Layer {
        public static LayerMask PLAYER { get => 1; }
        public static LayerMask ENEMY { get => 2; }
    }
}
