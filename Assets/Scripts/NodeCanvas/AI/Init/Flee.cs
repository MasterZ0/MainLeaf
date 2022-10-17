using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Pathfinding;
using UnityEngine;

namespace AdventureGame.NodeCanvas.AI
{
    [Category(Categories.AIPath)]
    [Description("TODO")]
    public class Flee : ActionTask<AIPath>
    {
        public BBParameter<Vector3> target;
        public BBParameter<float> escapeDistance;

        protected override void OnExecute()
        {
            agent.canMove = true;
            agent.canSearch = true;
        }

        protected override void OnUpdate()
        {
            Vector3 oppositeDirection = (agent.transform.position - target.value).normalized * escapeDistance.value;
            agent.destination = agent.transform.position + oppositeDirection;

            if (Vector3.Distance(agent.transform.position, target.value) >= escapeDistance.value)
            {
                EndAction();
            }
        }

        protected override void OnStop()
        {
            agent.canMove = false;
            agent.canSearch = false;
        }
    }
}