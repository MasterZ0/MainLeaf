using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using AdventureGame.Shared.NodeCanvas;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [NodeCategory(Categories.Variables)]
    [NodeDescription("Clamp a float between two values.")]
    public class ClampFloat : ActionTask
    {
        public Parameter<float> value;
        public Parameter<float> min;
        public Parameter<float> max;

        protected override void StartAction() {
            value.Value = Mathf.Clamp(value.Value, min.Value, max.Value);
            EndAction(true);
        }
    }
}