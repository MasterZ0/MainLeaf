using UnityEngine;
using AdventureGame.Shared;
using System.Collections.Generic;
using Z3.UIBuilder.Core;

namespace AdventureGame.Inputs.Data
{
    [CreateAssetMenu(menuName = Shared.MenuPath.Inputs + "Sprite Reference Static", fileName = "New" + nameof(InputSpriteReferenceStatic))]
    public class InputSpriteReferenceStatic : InputSpriteReference
    {
        //[DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine)]
        public Dictionary<DeviceController, Sprite> inputActions = new Dictionary<DeviceController, Sprite>();
    }
}
