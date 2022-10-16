﻿using FMODUnity;
using UnityEngine;
using Sirenix.OdinInspector;

namespace AdventureGame.Audio
{
    /// <summary>
    /// Pure sound class. Works just lite the SoundData, but it is not a ScriptableObject.
    /// Store a SoundInstance through PlaySound to have more control of it, if needed.
    /// </summary>
    [System.Serializable, InlineProperty, HideLabel] // There is a drawer called SoundReferenceDrawer.cs
    public class SoundReference
    {
        [SerializeField] private EventReference eventReference;

#if UNITY_EDITOR
        public string Path => eventReference.Path;
#else
        public string Path => string.Empty;
#endif
        public bool IsNull => eventReference.IsNull;

        internal FMOD.GUID Guid => eventReference.Guid;

        public SoundInstance PlaySound(Transform transform = null)
        {
            return AudioManager.PlaySound(eventReference, transform);
        }

        public SoundInstance PlaySound(Vector2 position)
        {
            return AudioManager.PlaySound(eventReference, position);
        }

        public static implicit operator bool(SoundReference thisReference)
        {
            if (thisReference.eventReference.IsNull)
                return false;
            else
                return true;
        }

        #region Operators
        public static bool operator ==(SoundReference a, SoundReference b)
        {
            return a.Guid == b.Guid;
        }

        public static bool operator ==(SoundInstance instance, SoundReference reference)
        {
            if (instance && reference)
                return instance.Guid == reference.Guid;

            if (!instance && !reference) // Both is null
                return true;

            return false;
        }

        public static bool operator !=(SoundReference a, SoundReference b) => !(a == b);
        public static bool operator !=(SoundInstance a, SoundReference b) => !(a == b);       
        public static bool operator ==(SoundReference a, SoundInstance b) => b == a;
        public static bool operator !=(SoundReference a, SoundInstance b) => !(b == a);
        #endregion
    }
}