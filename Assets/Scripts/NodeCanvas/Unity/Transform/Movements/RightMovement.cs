using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Movement)]
    [Description("Moves a rigidbody in the Transform.Right direction. This movement preserves the current speed in Y.")]
    public class RightMovement : ActionTask<Rigidbody2D>
    {
        public BBParameter<float> speed;
        protected override string info => $"Right Movement = {speed}";
        protected override void OnExecute()
        {
            float x = agent.transform.right.x * speed.value;
            agent.velocity = new Vector2(x, agent.velocity.y);
            EndAction(true);
        }
    }
}