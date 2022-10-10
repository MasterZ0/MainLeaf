using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity {

    [Category(Categories.Rigidbody2D)]
    [Description("Set Rigidbody2D velocity")]
    public class SetVelocityXY : ActionTask<Rigidbody2D> {

        public BBParameter<float> speedX;
        public BBParameter<float> speedY;
        protected override string info => $"Velocity = ({speedX}, {speedY})";
        protected override void OnExecute() {
            agent.velocity = new Vector2(speedX.value, speedY.value);
            EndAction(true);
        }
    }
}