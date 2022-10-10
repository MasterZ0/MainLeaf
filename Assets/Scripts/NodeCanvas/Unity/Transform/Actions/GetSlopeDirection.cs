using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using Header = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Unity.Physic
{

    [Category(Categories.Transform)]
    [Name("Get Slope Direction")]
    [Description("Returns Slope Direction Vector, Angle and boolean. Input Layer and the max angle that's considered a slope")]
    public class GetSlopeDirection : ActionTask<Transform>
    {
        [Header("Inputs")]
        [Tooltip("Slope Check Point")]
        public BBParameter<Transform> slopeCheckPoint;
        [Tooltip("Collision layer to check for")]
        public BBParameter<LayerMask> groundLayer;
        [Tooltip("Max angle to be considered a slope")]
        public BBParameter<float> maxSlopeAngle;

        [Header("OutPuts")]
        [BlackboardOnly]
        public BBParameter<Vector2> slopeDirection;
        [BlackboardOnly]
        public BBParameter<float> slopeAngle;
        [BlackboardOnly]
        public BBParameter<bool> isCurrentlyOnSlope;


        protected override string info => "Get Slope Direction";

        protected override void OnExecute()
        {
            float slopeDownAngle = 0f;
            bool isOnSlope = false;
            Vector2 slopeNormalPerpendicular = Vector2.zero;
            //Vector2 checkPosition = agent.position - new Vector3(0f, collider.value.size.y / 2 + collider.value.offset.y);
            Vector2 checkPosition = slopeCheckPoint.value.position;

            float slopeCheckDistance = 0.5f;

            RaycastHit2D hit = Physics2D.Raycast(checkPosition, Vector2.down, slopeCheckDistance, groundLayer.value);

            if (hit)
            {
                slopeNormalPerpendicular = Vector2.Perpendicular(hit.normal).normalized;
                slopeDownAngle = -Vector2.SignedAngle(hit.normal, Vector2.up);

                if (Mathf.Abs(slopeDownAngle) >= maxSlopeAngle.value)
                    isOnSlope = true;
                else
                    isOnSlope = false;

                Debug.DrawRay(hit.point, slopeNormalPerpendicular, Color.red);
                Debug.DrawRay(hit.point, hit.normal, Color.green);
            }

            slopeDirection.value = slopeNormalPerpendicular;
            slopeAngle.value = slopeDownAngle;
            isCurrentlyOnSlope.value = isOnSlope;

            EndAction();
        }
    }
}