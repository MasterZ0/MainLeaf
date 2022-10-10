using AdventureGame.Shared;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureGame.AI
{

    //Need to check the angle math later (sometimes the detection works when he is not supposed to. Probably something related
    //to distance.

    /// <summary>
    /// Detects a target within an angle range
    /// </summary>
    public class ViewAngle : ViewDetection
    {
        [Title("Field of View")]
        [SerializeField] private LayerMask targetLayers;
        [SerializeField, Range(0f, 10f)] private float radius;
        [SerializeField, Range(0f, 360f)] private float angle;

        private Vector3 Center => transform.position;

        public override bool FindTargetInsideRange(out Transform target)
        {
            //Check if target is within the view range
            Collider2D[] targets = Physics2D.OverlapCircleAll(Center, radius, targetLayers);
            target = null;

            foreach (Collider2D tempTargetCollider in targets)
            {
                if(!TargetBoundsWithinArc(tempTargetCollider))
                    continue;

                if (CanSeeTarget(tempTargetCollider)) //Check for walls
                {
                    target = tempTargetCollider.transform;
                    return true;
                }
            }
            return false;
        }

        private bool TargetBoundsWithinArc(Collider2D targetCol)
        {
            Vector3 BottomRightBound = new Vector2(targetCol.bounds.min.x, targetCol.bounds.max.y);
            Vector3 TopLeftBound = new Vector2(targetCol.bounds.max.x, targetCol.bounds.min.y);

            Vector2 directionBottomLeftBound = (targetCol.bounds.min - Center).normalized;
            Vector2 directionTopRightBound = (targetCol.bounds.max - Center).normalized;
            Vector2 directionBottomRightBound = (BottomRightBound - Center).normalized;
            Vector2 directionTopLeftBound = (TopLeftBound - Center).normalized;

            if (WithinAngle(directionBottomLeftBound))
                return true;
            if (WithinAngle(directionTopRightBound))
                return true;
            if (WithinAngle(directionBottomRightBound))
                return true;
            if (WithinAngle(directionTopLeftBound))
                return true;

            return false;
        }

        private bool WithinAngle(Vector2 directionToCheck)
        {
            float targetAngle = Vector2.Angle(transform.right, directionToCheck);
            if (targetAngle < angle / 2f)
                return true;
            return false;
        }

        private void OnDrawGizmos()
        {
            bool targetFound = FindTargetInsideRange(out Transform target);
            Color arcColor = targetFound ? new Color(0f, 1f, 0.34f, 0.1f) : new Color(1f, 0.11f, 0.21f, 0.1f);
            Gizmos.color = arcColor;

            if (targetFound)
                Gizmos.DrawLine(Center, target.position);

            transform.DrawArc(angle, radius, arcColor);
            transform.DrawWireArc(360f, radius, arcColor);
            Gizmos.DrawLine(Center, Center + transform.right * radius);
        }
    }
}