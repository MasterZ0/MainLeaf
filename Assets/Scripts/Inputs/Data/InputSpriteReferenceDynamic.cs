using UnityEngine;
using UnityEngine.InputSystem;
using AdventureGame.Shared;

namespace AdventureGame.Inputs.Data
{
    [CreateAssetMenu(menuName = MenuPath.Inputs + "Sprite Reference Dynamic", fileName = "New" + nameof(InputSpriteReferenceDynamic))]
    public class InputSpriteReferenceDynamic : InputSpriteReference
    {
        public InputActionReference inputAction;
    }
}
