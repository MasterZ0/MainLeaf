using AdventureGame.Shared.NodeCanvas;
using AdventureGame.Shared.Utils;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Rigidbody)]
    [Description("Set Rigidbody velocity by angle")]
    public class SetVelocityAngle : ActionTask<Rigidbody>
    {
        public BBParameter<Axis3> axis = Axis3.Z;
        public BBParameter<float> velocity;
        public BBParameter<float> angle;
        protected override string info => $"Velocity Angle = {velocity}";
        protected override void OnExecute()
        {
            // TODO: Review, use axis?
            Quaternion redAxisRotation = Quaternion.AngleAxis(angle.value, agent.transform.right);

            float finalAngle = redAxisRotation.eulerAngles.x + agent.transform.eulerAngles.y;

            agent.velocity = MathUtils.AngleToDirection(finalAngle, velocity.value);
            EndAction(true);
        }
    }
}