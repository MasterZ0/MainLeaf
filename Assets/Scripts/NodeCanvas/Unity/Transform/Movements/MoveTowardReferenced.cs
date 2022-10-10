using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Movement)]
    [Description("Move a GameObject to the target from the Agent position.")]
    public class MoveTowardReferenced : ActionTask<Transform> 
    {
        public BBParameter<Vector3> targetPosition;
        public BBParameter<float> speed;

        private Vector3 target;
        private const float ThresholdDistance = 0.02f;

        protected override string info => $"Move Referenced {targetPosition}";

        protected override void OnExecute() 
        {
            target = new Vector3()
            {
                x = agent.right.x * targetPosition.value.x + agent.position.x,
                y = agent.up.y * targetPosition.value.y + agent.position.y
            };
        }

        protected override void OnUpdate() 
        {
            agent.position = Vector2.MoveTowards(agent.position, target, Time.fixedDeltaTime * speed.value);

            if (Vector2.Distance(agent.position, target) < ThresholdDistance) 
            {
                EndAction(true);
            }
        }
    }
}