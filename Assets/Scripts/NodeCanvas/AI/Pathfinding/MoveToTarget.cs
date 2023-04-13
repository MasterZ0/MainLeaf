using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using Pathfinding;
using UnityEngine;

namespace AdventureGame.NodeCanvas.AI.Pathfinding
{
    [NodeCategory(Categories.Pathfinding)]
    [NodeDescription("TODO")]
    public class MoveToTarget : ActionTask<AIPath>
    {
        public Parameter<Vector3> target;

        public override string Info => $"Move To {target}";

        protected override void StartAction()
        {
            Agent.canMove = true;
            Agent.canSearch = true;
        }

        protected override void UpdateAction()
        {
            Agent.destination = target.Value;

            if (Agent.reachedDestination)
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