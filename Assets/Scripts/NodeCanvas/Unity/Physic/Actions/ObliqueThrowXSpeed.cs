using NodeCanvas.Framework;
using AdventureGame.Shared.NodeCanvas;
using ParadoxNotion.Design;
using UnityEngine;
using AdventureGame.Shared.Utils;

namespace AdventureGame.NodeCanvas.Unity {

    [Category(Categories.Rigidbody2D)]
    [Description("Please describe what this ActionTask does.")]
    [Name("Oblique Throw X Speed")]
    public class ObliqueThrowXSpeed : ActionTask<Rigidbody2D> {

        public BBParameter<Vector3> distance;
        public BBParameter<float> xSpeed;

        protected override void OnExecute()
        {
            agent.velocity = MathUtils.ObliqueThrowX(distance.value, agent.gravityScale, xSpeed.value);
            EndAction(true);
        }
    }
}