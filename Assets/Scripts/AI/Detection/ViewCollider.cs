using System.Collections.Generic;
using UnityEngine;


namespace AdventureGame.AI
{
    /// <summary>
    /// Detects a target within a collider component and check if the enemy can see him.
    /// </summary>
    public class ViewCollider : ViewDetection
    {
        private readonly List<Collider2D> colsInside = new List<Collider2D>();

        public override bool FindTargetInsideRange(out Transform target)
        {
            target = null;
            foreach (Collider2D colInside in colsInside)
            {
                if (CanSeeTarget(colInside) && colInside.attachedRigidbody != null)
                {
                    target = colInside.attachedRigidbody.transform;
                    return true;
                }
            }
            return false;
        }

        private void OnTriggerEnter2D(Collider2D col) => colsInside.Add(col);
        private void OnTriggerExit2D(Collider2D col) => colsInside.Remove(col);
    }
}