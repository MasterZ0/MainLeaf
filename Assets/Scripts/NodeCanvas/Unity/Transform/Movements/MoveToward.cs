using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity {

    [Category(Categories.Movement)]
    [Description("Move a GameObject to the target position.")]
    public class MoveToward : ActionTask<Transform> 
    {
        public BBParameter<Vector3> targetPosition;
        public BBParameter<float> speed;

        protected override string info => $"Move To {targetPosition}";

        private const float ThresholdDistance = 0.02f;

        protected override void OnUpdate() 
        {
            agent.position = Vector3.MoveTowards(agent.position, targetPosition.value, Time.fixedDeltaTime * speed.value);

            if (Vector3.Distance(agent.position, targetPosition.value) < ThresholdDistance) 
            {
                EndAction();
            }
        }
    }
}