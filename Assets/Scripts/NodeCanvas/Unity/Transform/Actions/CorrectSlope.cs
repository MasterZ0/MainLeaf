using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using Header = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Unity.Physic
{
    [Category(Categories.Transform)]
    [Description("Correct a body based on current slope")]
    public class CorrectSlope : ActionTask<Transform>
    {
        [Header("Inputs")]
        public BBParameter<Vector3> slopeCheckPoint;
        public BBParameter<LayerMask> groundLayer;
        public BBParameter<float> speed;
        public BBParameter<float> maxSlopeAngle = 45f;
        public BBParameter<float> slopeCheckDistance = 0.5f;

        protected override void OnExecute()
        {
            float slopeDownAngle = CheckSlope();
            float currentDegrees = Mathf.MoveTowardsAngle(agent.eulerAngles.z, slopeDownAngle, Time.fixedDeltaTime * speed.value);
            agent.rotation = Quaternion.Euler(agent.eulerAngles.x, agent.eulerAngles.y, currentDegrees);
            
            EndAction(true);
        }


        private float CheckSlope()
        {
            RaycastHit2D hit = Physics2D.Raycast(slopeCheckPoint.value, Vector2.down, slopeCheckDistance.value, groundLayer.value);
            
            if (hit)
            {
                float slopeAngle = -Vector2.SignedAngle(hit.normal, Vector2.up);

                if (Mathf.Abs(slopeAngle) <= maxSlopeAngle.value)
                {
                    return agent.right.x > 0 ? slopeAngle : -slopeAngle;
                }
            }

            return 0f;
        }
    }
}