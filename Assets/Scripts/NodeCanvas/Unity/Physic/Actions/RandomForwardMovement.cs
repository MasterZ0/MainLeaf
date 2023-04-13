using AdventureGame.Shared.ExtensionMethods;
using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity.Physic
{
    [NodeCategory(Categories.Movement)]
    [NodeDescription("Moves a rigidbody in the Transform.Right direction with random speed within a range. This movement preserves the current speed in Y.")]
    public class RandomForwardMovement : ActionTask<Rigidbody2D>
    {
        public Parameter<Vector2> speedRange;
        public override string Info => $"Random Forward Movement = {speedRange}";
        protected override void StartAction()
        {
            Vector3 forward = Agent.transform.forward * speedRange.Value.RandomRange();
            Agent.velocity = new Vector3(forward.x, Agent.velocity.y, forward.z);
            EndAction(true);
        }
    }
}