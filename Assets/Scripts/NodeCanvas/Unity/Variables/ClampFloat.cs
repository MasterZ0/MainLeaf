using NodeCanvas.Framework;
using ParadoxNotion.Design;
using AdventureGame.Shared.NodeCanvas;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Variables)]
    [Description("Clamp a float between two values.")]
    public class ClampFloat : ActionTask
    {
        public BBParameter<float> value;
        public BBParameter<float> min;
        public BBParameter<float> max;

        protected override void OnExecute() {
            value.value = Mathf.Clamp(value.value, min.value, max.value);
            EndAction(true);
        }
    }
}