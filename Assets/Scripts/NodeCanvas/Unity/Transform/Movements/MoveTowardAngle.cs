using AdventureGame.Shared.NodeCanvas;
using AdventureGame.Shared.Utils;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity {

    [Category(Categories.Movement)]
    [Description("Move a GameObject to the target position.")]
    public class MoveTowardAngle : ActionTask<Transform> {

        public BBParameter<Axis> axis = Axis.Z;
        public BBParameter<float> speed;
        public BBParameter<float> angle;
        public BBParameter<float> distance;

        private Vector2 target;
        private const float ThresholdDistance = 0.02f;

        protected override string info => agent != null ? $"Move To {angle}º" : name;
        private Vector2 Target => MathUtils.AngleToDirection(angle.value, distance.value) + (Vector2)agent.position;

        protected override void OnExecute() {
            target = Target;
        }


        protected override void OnUpdate() {

            agent.position = Vector3.MoveTowards(agent.position, target, Time.fixedDeltaTime * speed.value);

            if (Vector3.Distance(agent.position, target) < ThresholdDistance) {
                agent.position = target;
                EndAction(true);
            }
        }
    }
}