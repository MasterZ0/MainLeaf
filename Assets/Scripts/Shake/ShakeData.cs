using AdventureGame.Shared;
using UnityEngine;

namespace AdventureGame.Shake
{
    /// <summary>
    /// ScriptableObject to store ShakeParameters
    /// </summary>
    [CreateAssetMenu(menuName = MenuPath.ScriptableObjects + "Shake", fileName = "NewShakeData")]
    public class ShakeData : ScriptableObject
    {
        //[/*InlineProperty,*/ HideLabel]
        public ShakeParameters shakeParameters;

        public static implicit operator ShakeParameters(ShakeData shakeData) => shakeData.shakeParameters;
    }
}

