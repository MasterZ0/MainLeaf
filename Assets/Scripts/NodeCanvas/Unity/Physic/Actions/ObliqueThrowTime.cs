using AdventureGame.Shared.NodeCanvas;
using AdventureGame.Shared.Utils;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity {

    [Category(Categories.Rigidbody2D)]
    [Description("Set the agent's oblique velocity according to time, distance and the agent's gravity.")]
    public class ObliqueThrowTime : ActionTask<Rigidbody2D> {

        public bool waitFinish;
        public BBParameter<Vector3> distance;
        public BBParameter<float> time;

        protected override void OnExecute()
        {
            agent.velocity = MathUtils.ObliqueThrowTime(distance.value, agent.gravityScale, time.value);

            if (!waitFinish) {
                EndAction(true);
            }
        }

        protected override void OnUpdate() {
            if (elapsedTime >= time.value) {
                EndAction(true);
            }
        }
    }
}