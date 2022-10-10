using Cinemachine;
using System.Linq;
using UnityEngine;
using static Cinemachine.NoiseSettings;

namespace AdventureGame.Shake
{
    /// <summary>
    /// A data structure for holding shake parameters
    /// </summary>
    public class ShakeResult
    {
        public float power;
        public TransformNoiseParams[] positionNoise = new TransformNoiseParams[0];
        public TransformNoiseParams[] orientationNoise = new TransformNoiseParams[0];

        public static ShakeResult operator +(ShakeResult a, ShakeResult b)
        {
            return new ShakeResult
            {
                positionNoise = a.positionNoise.Concat(b.positionNoise).ToArray(),
                orientationNoise = a.orientationNoise.Concat(b.orientationNoise).ToArray(),
                power = a.power > b.power ? a.power : b.power
            };
        }

        public static implicit operator NoiseSettings(ShakeResult shakeResult)
        {
            var noise = ScriptableObject.CreateInstance<NoiseSettings>();

            noise.PositionNoise = shakeResult.positionNoise;
            noise.OrientationNoise = shakeResult.orientationNoise;

            return noise;
        }
    }
}
