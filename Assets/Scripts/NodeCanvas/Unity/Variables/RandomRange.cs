using AdventureGame.Shared.ExtensionMethods;
using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using HeaderAttribute = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Unity {

    [Category(Categories.Variables)]
    [Description("Return a random value between range.x and range.y")]
    public class RandomRange : ActionTask {

        [Header("In")]
        public BBParameter<Vector2> range;

        [Header("Out")]
        public BBParameter<float> result;

        protected override string info => range.isDefined ?
            $"Random Range {range}" :
            $"Random ({range.value.x} - {range.value.y})";

        protected override void OnExecute() {
            result.value = range.value.RandomRange();
            EndAction(true);
        }
    }
}