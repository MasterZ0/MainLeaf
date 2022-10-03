using FMODUnity;
using UnityEngine;
using AdventureGame.Shared;

namespace AdventureGame.Audio {

    /// <summary>
    /// SoundReference Scriptable Object. You can give position through Transform, Vector2 or nothing at all.
    /// Store a SoundInstance through PlaySound to have more control of it, if needed.
    /// </summary>
    [CreateAssetMenu(menuName = MenuPath.ScriptableObjects + "Sound Data", fileName = "NewSoundData")]
    public class SoundData : ScriptableObject {

        [SerializeField] private EventReference eventReference;

        public SoundInstance PlaySound(Transform transform = null)
        {
            return AudioManager.PlaySound(eventReference, transform);
        }

        public SoundInstance PlaySound(Vector2 position)
        {
            return AudioManager.PlaySound(eventReference, position);
        }
    }
}