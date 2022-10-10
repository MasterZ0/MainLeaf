using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Rigidbody2D)]
    [Description("Get Rigidbody Velocity decomposed")]
    public class GetVelocityXY : ActionTask<Rigidbody2D>
    {
        public BBParameter<float> xVelocity;
        public BBParameter<float> yVelocity;

        protected override string info => $"Get {agentInfo} XY Velocity";
        protected override void OnExecute() {
            xVelocity.value = agent.velocity.x;
            yVelocity.value = agent.velocity.y;
            EndAction(true);
        }
    }
}