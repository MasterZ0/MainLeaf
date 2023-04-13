using AdventureGame.Shared.NodeCanvas;
using AdventureGame.Shared.Utils;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [NodeCategory(Categories.Rigidbody)]
    [NodeDescription("Set Rigidbody velocity by angle")]
    public class SetVelocityAngle : ActionTask<Rigidbody>
    {
        public Parameter<Axis3> axis = Axis3.Z;
        public Parameter<float> velocity;
        public Parameter<float> angle;
        public override string Info => $"Velocity Angle = {velocity}";
        protected override void StartAction()
        {
            // TODO: Review, use axis?
            Quaternion redAxisRotation = Quaternion.AngleAxis(angle.Value, Agent.transform.right);

            float finalAngle = redAxisRotation.eulerAngles.x + Agent.transform.eulerAngles.y;

            Agent.velocity = MathUtils.AngleToDirection(finalAngle, velocity.Value);
            EndAction(true);
        }
    }
}