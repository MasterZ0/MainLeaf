using AdventureGame.Shared.NodeCanvas;
using AdventureGame.Shared.Utils;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Rigidbody2D)]
    [Description("Set Rigidbody2D velocity by angle and the current orientation")]
    public class SetVelocityAngleReferenced : ActionTask<Rigidbody2D>  // Used in Black Virtue Kick
    {
        
        public BBParameter<float> velocity;
        public BBParameter<float> angle;
        protected override string info => $"Velocity Angle Ref = {velocity}";
        protected override void OnExecute() {
            Quaternion redAxisRotation = Quaternion.AngleAxis(angle.value, agent.transform.right);

            // TODO: It works only Y = 0 or 180, no make sense
            float finalAngle = redAxisRotation.eulerAngles.x + agent.transform.eulerAngles.y;

            agent.velocity = MathUtils.AngleToDirection(finalAngle, velocity.value);

            EndAction(true);
        }
    }
}