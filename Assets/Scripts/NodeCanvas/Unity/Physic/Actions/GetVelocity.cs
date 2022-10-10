using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Rigidbody2D)]
    [Description("Get Rigidbody Velocity")]
    public class GetVelocity : ActionTask<Rigidbody2D>
    {
        public BBParameter<Vector2> velocity;

        protected override string info => $"Get {agentInfo} Velocity";
        protected override void OnExecute() {
            velocity.value = agent.velocity;
            EndAction(true);
        }
    }
}