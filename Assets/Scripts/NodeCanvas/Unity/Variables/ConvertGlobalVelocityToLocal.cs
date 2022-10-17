using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Transform)]
    [Description("Convert the global velocity to the velocity the transform is directed")]
    public class ConvertGlobalVelocityToLocal : ActionTask<Transform>
    {
        public BBParameter<Vector3> globalVelocity;
        public BBParameter<Vector3> localVelocity;

        protected override string info => $"Convert {globalVelocity} to Velocity";

        protected override void OnExecute()
        {
            localVelocity.value = new Vector3()
            {
                x = Vector3.Dot(agent.right, globalVelocity.value),
                y = Vector3.Dot(agent.up, globalVelocity.value),
                z = Vector3.Dot(agent.forward, globalVelocity.value)
            };

            EndAction();
        }
    }
}