using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using AdventureGame.Shared.NodeCanvas;

namespace AdventureGame.NodeCanvas.Unity.Physic
{
    [Category(Categories.Rigidbody2D)]
    [Description("Set Rigidbody2D velocity in the X axis.")]
    public class SetVelocityX : ActionTask<Rigidbody2D>
    {
        public BBParameter<float> speedX;

        protected override string info => $"Velocity's X = {speedX}";
        protected override void OnExecute()
        {
            agent.velocity = new Vector2(speedX.value, agent.velocity.y);
            EndAction(true);
        }
    }
}