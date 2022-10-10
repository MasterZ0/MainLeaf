using AdventureGame.Shared.ExtensionMethods;
using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Movement)]
    [Description("Moves a rigidbody in the Transform.Right direction with random speed within a range. This movement preserves the current speed in Y.")]
    public class RandomRightMovement : ActionTask<Rigidbody2D> 
    {
        public BBParameter<Vector2> speedRange;
        protected override string info => $"Random Right Movement = {speedRange}";
        protected override void OnExecute()
        {
            float x = agent.transform.right.x * speedRange.value.RandomRange();
            agent.velocity = new Vector2(x, agent.velocity.y);
            EndAction(true);
        }
    }
}