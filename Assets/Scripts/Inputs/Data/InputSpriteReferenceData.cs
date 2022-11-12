using UnityEngine;
using UnityEngine.InputSystem;
using AdventureGame.Shared;

namespace AdventureGame.Inputs.Data
{
    [CreateAssetMenu(menuName = MenuPath.Inputs + "Sprite Reference", fileName = "NewInputSpriteData")]
    public class InputSpriteReferenceData : ScriptableObject 
    {
        public InputActionReference inputActionReference;
    }
}
