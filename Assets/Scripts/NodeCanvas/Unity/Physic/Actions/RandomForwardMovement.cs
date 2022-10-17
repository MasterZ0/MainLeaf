using AdventureGame.Shared.ExtensionMethods;
using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity.Physic
{
    [Category(Categories.Movement)]
    [Description("Moves a rigidbody in the Transform.Right direction with random speed within a range. This movement preserves the current speed in Y.")]
    public class RandomForwardMovement : ActionTask<Rigidbody2D>
    {
        public BBParameter<Vector2> speedRange;
        protected override string info => $"Random Forward Movement = {speedRange}";
        protected override void OnExecute()
        {
            Vector3 forward = agent.transform.forward * speedRange.value.RandomRange();
            agent.velocity = new Vector3(forward.x, agent.velocity.y, forward.z);
            EndAction(true);
        }
    }
}