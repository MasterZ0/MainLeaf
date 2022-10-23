using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Pathfinding;
using UnityEngine;

namespace AdventureGame.NodeCanvas.AI.Pathfinding
{
    [Category(Categories.Pathfinding)]
    [Description("TODO")]
    public class MoveToTarget : ActionTask<AIPath>
    {
        public BBParameter<Vector3> target;

        protected override string info => $"Move To {target}";

        protected override void OnExecute()
        {
            agent.canMove = true;
            agent.canSearch = true;
        }

        protected override void OnUpdate()
        {
            agent.destination = target.value;

            if (agent.reachedDestination)
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