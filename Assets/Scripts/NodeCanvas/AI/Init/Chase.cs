using AdventureGame.Data;
using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Pathfinding;
using UnityEngine;

namespace AdventureGame.NodeCanvas.AI
{


    [Category(Categories.AIPath)]
    [Description("Chase")]
    public class Chase : ActionTask<AIPath>
    {
        public BBParameter<Transform> target;
        public BBParameter<float> distanceToStop;

        protected override void OnExecute()
        {
            //agent.repathRate = GameSettings.Enemies.RepathRate;
            agent.canMove = true;
        }

        protected override void OnUpdate()
        {
            agent.destination = target.value.position;

            if (Vector3.Distance(agent.transform.position, target.value.position) <= distanceToStop.value)
            {
                agent.canMove = false;
                // Check rotation
                EndAction();
            }
        }
    }
}