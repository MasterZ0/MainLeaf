using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using Header = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Unity.Physic
{
    [Category(Categories.Rigidbody2D)]
    [Description("Set Rigidbody2D velocity on slope. Use this with GetSlopeDirection Node.")]
    public class SetVelocityOnSlope : ActionTask<Rigidbody2D>
    {
        [Header("Inputs")]
        public BBParameter<float> moveSpeed;
        public BBParameter<Vector2> slopeDirection;
        public BBParameter<Transform> target;

        protected override string info => $"Velocity On Slope = {moveSpeed} * {slopeDirection}";
        protected override void OnExecute()
        {
            Vector2 newVelocity = new Vector2();
            newVelocity.Set(-moveSpeed.value * slopeDirection.value.x, -moveSpeed.value * slopeDirection.value.y);

            //if (directionConditions.value != TargetConditions.DontUseTarget)
            //{
            //    bool targetPositionIsBigger = target.value.position.x > agent.transform.position.x;

            //    if (directionConditions.value == TargetConditions.TorwardsTarget && !targetPositionIsBigger)
            //        newVelocity *= -1;
            //    else if (directionConditions.value == TargetConditions.AgainstTarget && targetPositionIsBigger)
            //        newVelocity *= -1;
            //}

            agent.velocity = newVelocity;
            EndAction(true);
        }
    }
}