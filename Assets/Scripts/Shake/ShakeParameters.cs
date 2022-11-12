using Cinemachine;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace AdventureGame.Shake
{
    /// <summary>
    /// Store the shake parameters
    /// </summary>
    [Serializable]
    public class ShakeParameters
    {
        [Title("Shake Parameters")]
        [SerializeField] private int sortOrder;
        [Space]
        [Range(0, 10)]
        [SerializeField] private float fadeIn;
        [Min(0)]
        [SerializeField] private float duration = 1;
        [Range(0, 10)]
        [SerializeField] private float fadeOut;

        [ShowInInspector, PropertyOrder(1)]
        public float TotalDuration => fadeIn + duration + fadeOut;

        [InlineEditor, PropertyOrder(2)]
        public NoiseSettings noiseSettings;

        public int SortOrder => sortOrder;
        public float FadeIn => fadeIn;
        public float FadeOut => fadeOut;
        public float Duration => duration;
    }
}
