using AdventureGame.Shared.NodeCanvas;
using AdventureGame.Shared.Utils;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Rigidbody2D)]
    [Description("Set Rigidbody2D velocity by angle")]
    public class SetVelocityAngle : ActionTask<Rigidbody2D>
    {
        public BBParameter<float> velocity;
        public BBParameter<float> angle;
        protected override string info => $"Velocity Angle = {velocity}";
        protected override void OnExecute()
        {
            agent.velocity = MathUtils.AngleToDirection(angle.value, velocity.value);
            EndAction(true);
        }
    }
}