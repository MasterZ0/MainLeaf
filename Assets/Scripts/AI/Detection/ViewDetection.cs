using UnityEngine;
using Sirenix.OdinInspector;

namespace AdventureGame.AI
{
    /// <summary>
    /// Base class to examine the visibility of a target, by raycasting its bounds.
    /// </summary>
    public abstract class ViewDetection : MonoBehaviour
    {
        [Title("Detection")]
        [SerializeField] protected LayerMask obstaclesLayer;

        public abstract bool FindTargetInsideRange(out Transform target);

        protected bool CanSeeTarget(Collider2D targetCol)
        {
            Vector2 BottomRightBound = new Vector2(targetCol.bounds.min.x, targetCol.bounds.max.y);
            Vector2 TopLeftBound = new Vector2(targetCol.bounds.max.x, targetCol.bounds.min.y);

            // Center is unnecessary, because the camera is too small
            if (CanSeePosition(targetCol.bounds.min))
                return true;
            if (CanSeePosition(targetCol.bounds.max))
                return true;
            if (CanSeePosition(BottomRightBound))
                return true;
            if (CanSeePosition(TopLeftBound))
                return true;

            return false;
        }

        private bool CanSeePosition(Vector3 target)
        {
            Vector2 direction = target - transform.position;
            float distance = Vector2.Distance(transform.position, target);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, obstaclesLayer);
            return !hit;
        }
    }
}