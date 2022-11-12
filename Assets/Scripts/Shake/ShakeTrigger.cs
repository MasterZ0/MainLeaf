using UnityEngine;

namespace AdventureGame.Shake
{
    /// <summary>
    /// Shake trigger for animation clips.
    /// </summary>
    public class ShakeTrigger : MonoBehaviour 
    {
        public void Shake(ShakeData shakeData) => Shaker.RequestShake(shakeData); 
    }
}