using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity {

    [Category(Categories.Rigidbody)]
    [Description("Set Rigidbody velocity")]
    public class SetVelocity : ActionTask<Rigidbody> {

        public BBParameter<Vector3> velocity;
        protected override string info => $"Velocity = {velocity}";
        protected override void OnExecute() {
            agent.velocity = velocity.value;
            EndAction(true);
        }
    }
}