using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Pathfinding;
using UnityEngine;

namespace AdventureGame.NodeCanvas.AI
{
    [Category(Categories.AIPath)]
    [Description("TODO")]
    public class GetDeltaMovement : ActionTask<Transform>
    {
        public BBParameter<Vector3> velocity;

        protected override string info => $"Get {agentInfo} AI Velocity";

        private Vector3 previousPosition;
        private int previousFrame;
        protected override void OnExecute()
        {
            previousFrame = Time.frameCount;
            previousPosition = agent.position;
        }

        protected override void OnUpdate()
        {
            if (Time.frameCount > previousFrame)
            {
                velocity.value = (agent.position - previousPosition) / Time.fixedDeltaTime;
                EndAction();
            }
        }
    }
}