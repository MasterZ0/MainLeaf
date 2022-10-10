using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity {

    [Category(Categories.Rigidbody2D)]
    [Description("Set Rigidbody2D gravity scale")]
    public class SetGravity : ActionTask<Rigidbody2D> {

        public BBParameter<float> gravity;

        protected override string info => $"Gravity = {gravity}";

        protected override void OnExecute() {
            agent.gravityScale = gravity.value;
            EndAction(true);
        }
    }
}