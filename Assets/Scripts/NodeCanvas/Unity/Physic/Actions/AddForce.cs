using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity.Physic
{
    [Category(Categories.Rigidbody2D)]
    [Description("Add Force")]
    public class AddForce : ActionTask<Rigidbody2D>
    {
        public BBParameter<Vector2> force;
        public BBParameter<ForceMode2D> forceMode = ForceMode2D.Force;

        protected override string info => $"Add Force = {force}, {forceMode}";

        protected override void OnExecute()
        {
            agent.AddForce(force.value, forceMode.value);
            EndAction(true);
        }
    }
}