using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using Pathfinding;
using UnityEngine;

namespace AdventureGame.NodeCanvas.AI.Pathfinding
{
    [NodeCategory(Categories.Pathfinding)]
    [NodeDescription("TODO")]
    public class Flee : ActionTask<AIPath>
    {
        public Parameter<Vector3> target;
        public Parameter<float> escapeDistance;

        protected override void StartAction()
        {
            Agent.canMove = true;
            Agent.canSearch = true;
        }

        protected override void UpdateAction()
        {
            Vector3 oppositeDirection = (Agent.transform.position - target.Value).normalized * escapeDistance.Value;
            Agent.destination = Agent.transform.position + oppositeDirection;

            if (Vector3.Distance(Agent.transform.position, target.Value) >= escapeDistance.Value)
            {
                EndAction();
            }
        }

        protected override void StopAction()
        {
            Agent.canMove = false;
            Agent.canSearch = false;
        }
    }
}