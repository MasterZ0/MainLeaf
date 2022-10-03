using UnityEngine.InputSystem;

namespace AdventureGame.Inputs
{
    /// <summary>
    /// Tutorial: https://www.youtube.com/watch?v=WSw82nKXibc
    /// </summary>
    public class Rumble
    {
        private static bool vibration;

        public static void SetActive(bool enable)
        {
            vibration = enable;
            if (!enable && Gamepad.current != null)
                Gamepad.current.SetMotorSpeeds(0f, 0f);
        }

        public static void SetRumble(float lowFrequency, float highFrequency)
        {
            if (!vibration || Gamepad.current == null)
                return;

            Gamepad.current.SetMotorSpeeds(lowFrequency, highFrequency);
        }
    }
}