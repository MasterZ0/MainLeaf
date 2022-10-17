using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Rigidbody)]
    [Description("Get Rigidbody Velocity")]
    public class GetRigidbodyVelocity : ActionTask<Rigidbody>
    {
        public BBParameter<Vector3> velocity;

        protected override string info => $"Get {agentInfo} Velocity";
        protected override void OnExecute() 
        {
            velocity.value = agent.velocity;
            EndAction(true);
        }
    }
}