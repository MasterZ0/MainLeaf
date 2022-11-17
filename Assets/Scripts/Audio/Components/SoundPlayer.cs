using UnityEngine;

namespace AdventureGame.Audio
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private SoundReference soundReference;

        private SoundInstance instance;

        private void OnEnable()
        {
            instance = soundReference.PlaySound(transform);
            AudioManager.AddToPauseSoundsList(instance);
        }

        private void OnDisable()
        {
            instance.StopWithFade();
        }
    }
}