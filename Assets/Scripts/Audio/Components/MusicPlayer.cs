using UnityEngine;

namespace AdventureGame.Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private SoundReference soundReference;

        private void Awake()
        {
            AudioManager.SetCurrentMusic(soundReference);
        }

        private void OnDisable()
        {
            AudioManager.SetCurrentMusic(null);
        }
    }
}