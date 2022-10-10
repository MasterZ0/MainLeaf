using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity {

    [Category(Categories.Rigidbody2D)]
    [Description("Set Rigidbody2D velocity")]
    public class SetVelocity : ActionTask<Rigidbody2D> {

        public BBParameter<Vector2> velocity;
        protected override string info => $"Velocity = {velocity}";
        protected override void OnExecute() {
            agent.velocity = velocity.value;
            EndAction(true);
        }
    }
}