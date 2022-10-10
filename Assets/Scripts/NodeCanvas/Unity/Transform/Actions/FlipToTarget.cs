using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity {

    [Category(Categories.Transform)]
    [Description("Change the Y rotation based on target")]
    public class FlipToTarget : ActionTask<Transform> 
    {
        public BBParameter<Vector3> target;

        protected override string info => $"Flip To {target}";

        protected override void OnExecute() {
            LookAtTarget();
            EndAction(true);
        }

        private void LookAtTarget() {
            bool lookingRight = agent.right.x > 0;
            bool targetRight = agent.position.x < target.value.x;

            if (lookingRight != targetRight) {
                agent.Rotate(0f, 180f, 0f);
            }
        }
    }
}