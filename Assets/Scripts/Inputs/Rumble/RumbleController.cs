using System.Collections;
using UnityEngine;

namespace AdventureGame.Inputs
{
    public class RumbleController : MonoBehaviour
    {
        private bool joystickVibrationActive;

        private void Rumbling(float totalTime, float lowFrequency, float highFrequency)
        {
            if (!joystickVibrationActive)
                return;

            StopAllCoroutines();
            Rumble.SetRumble(lowFrequency, highFrequency);

            StartCoroutine(StopRumble(totalTime));
        }
        private IEnumerator StopRumble(float time)
        {
            yield return new WaitForSecondsRealtime(time);
            Rumble.SetRumble(0f, 0f);
        }

        private void OnDestroy()
        {
            Rumble.SetRumble(0f, 0f);
        }
    }
}