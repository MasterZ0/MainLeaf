using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using AdventureGame.Shared.NodeCanvas;

namespace AdventureGame.NodeCanvas.Unity.Physic
{
    [Category(Categories.Rigidbody2D)]
    [Description("Set Rigidbody2D velocity in the Y axis.")]
    public class SetVelocityY : ActionTask<Rigidbody2D>
    {
        public BBParameter<float> speedY;

        protected override string info => $"Velocity's Y = ({speedY})";
        protected override void OnExecute()
        {
            agent.velocity = new Vector2(agent.velocity.x, speedY.value);
            EndAction(true);
        }
    }
}