using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity {

    [Category(Categories.Movement)]
    [Description("Move a GameObject to the target position.")]
    public class MoveToward : ActionTask<Transform> {

        [RequiredField] public BBParameter<Vector3> targetPosition;
        [RequiredField] public BBParameter<float> speed;

        private Vector2 target;
        private const float ThresholdDistance = 0.02f;

        protected override string info => $"Move To {targetPosition}";

        protected override void OnExecute() {
            target = targetPosition.value;
        }

        protected override void OnUpdate() {

            agent.position = Vector2.MoveTowards(agent.position, target, Time.fixedDeltaTime * speed.value);

            if (Vector2.Distance(agent.position, target) < ThresholdDistance) {
                EndAction(true);
            }
        }
    }
}