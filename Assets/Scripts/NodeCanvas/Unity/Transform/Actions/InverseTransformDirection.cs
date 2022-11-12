using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Transform)]
    [Description("Get Character Controller Velocity")]
    public class InverseTransformDirection : ActionTask
    {
        public BBParameter<Vector3> direction;
        public BBParameter<Vector3> inverse;

        protected override string info => $"Get {agentInfo} Velocity";

        protected override void OnExecute()
        {
            inverse.value = agent.transform.InverseTransformDirection(direction.value);
            EndAction();
        }
    }
}