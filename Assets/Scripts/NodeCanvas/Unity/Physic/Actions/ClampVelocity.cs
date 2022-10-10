using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using HeaderAttribute = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Unity.Physic
{
    [Category(Categories.Rigidbody2D)]
    [Description("Clamp Velocity to a set of values")]
    public class ClampVelocity : ActionTask<Rigidbody2D>
    {
        [Header("Inputs")]
        public BBParameter<Vector2> minVelocity;
        public BBParameter<Vector2> maxVelocity;
        protected override string info => $"Min Velocity = {minVelocity}, Max Velocity = {maxVelocity}";
        protected override void OnExecute()
        {
            float velocityX = Mathf.Clamp(agent.velocity.x, minVelocity.value.x, maxVelocity.value.x);
            float velocityY = Mathf.Clamp(agent.velocity.y, minVelocity.value.y, maxVelocity.value.y);

            agent.velocity = new Vector2(velocityX, velocityY);

            EndAction(true);
        }
    }
}