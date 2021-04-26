using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AI {
    [TaskCategory("AI")]
    public class CanHearTarget : Conditional {
        public bool usePhysics2D;
        public SharedGameObject targetObject;
        public SharedGameObjectList targetObjects;
        public SharedString targetTag;
        public LayerMask objectLayerMask;
        public int maxCollisionCount = 200;
        public SharedFloat hearingRadius = 50;
        public SharedFloat audibilityThreshold = 0.05f;
        public SharedVector3 offset;
        public SharedGameObject returnedObject;

        private Collider[] overlapColliders;
        private Collider2D[] overlap2DColliders;

        public override TaskStatus OnUpdate() {
            return TaskStatus.Failure;
        }

        /*
        // Returns success if an object was found otherwise failure
        public override TaskStatus OnUpdate() {
            if (targetObjects.Value != null && targetObjects.Value.Count > 0) { // If there are objects in the group list then search for the object within that list
                GameObject objectFound = null;
                for (int i = 0; i < targetObjects.Value.Count; ++i) {
                    float audibility = 0;
                    GameObject obj;
                    if (Vector3.Distance(targetObjects.Value[i].transform.position, transform.position) < hearingRadius.Value) {
                        if ((obj = MovementUtility.WithinHearingRange(transform, offset.Value, audibilityThreshold.Value, targetObjects.Value[i], ref audibility)) != null) {
                            objectFound = obj;
                        }
                    }
                }
                returnedObject.Value = objectFound;
            }
            else if (targetObject.Value == null) { // If the target object is null then determine if there are any objects within hearing range based on the layer mask
                if (usePhysics2D) {
                    if (overlap2DColliders == null) {
                        overlap2DColliders = new Collider2D[maxCollisionCount];
                    }
                    returnedObject.Value = MovementUtility.WithinHearingRange2D(transform, offset.Value, audibilityThreshold.Value, hearingRadius.Value, overlap2DColliders, objectLayerMask);
                }
                else {
                    if (overlapColliders == null) {
                        overlapColliders = new Collider[maxCollisionCount];
                    }
                    returnedObject.Value = MovementUtility.WithinHearingRange(transform, offset.Value, audibilityThreshold.Value, hearingRadius.Value, overlapColliders, objectLayerMask);
                }
            }
            else if (!string.IsNullOrEmpty(targetTag.Value)) {
                GameObject objectFound = null;
                var targets = GameObject.FindGameObjectsWithTag(targetTag.Value);
                for (int i = 0; i < targets.Length; ++i) {
                    float audibility = 0;
                    GameObject obj;
                    if (Vector3.Distance(targetObjects.Value[i].transform.position, transform.position) < hearingRadius.Value) {
                        if ((obj = MovementUtility.WithinHearingRange(transform, offset.Value, audibilityThreshold.Value, targetObjects.Value[i], ref audibility)) != null) {
                            objectFound = obj;
                        }
                    }
                }
                returnedObject.Value = objectFound;
            }
            else {
                var target = targetObject.Value;
                if (Vector3.Distance(target.transform.position, transform.position) < hearingRadius.Value) {
                    returnedObject.Value = MovementUtility.WithinHearingRange(transform, offset.Value, audibilityThreshold.Value, targetObject.Value);
                }
            }

            if (returnedObject.Value != null) {
                // returnedObject success if an object was heard
                return TaskStatus.Success;
            }
            // An object is not within heard so return failure
            return TaskStatus.Failure;
        }

        // Reset the public variables
        public override void OnReset() {
            hearingRadius = 50;
            audibilityThreshold = 0.05f;
        }

        // Draw the hearing radius
        public override void OnDrawGizmos() {
#if UNITY_EDITOR
            if (Owner == null || hearingRadius == null) {
                return;
            }
            var oldColor = UnityEditor.Handles.color;
            UnityEditor.Handles.color = Color.yellow;
            UnityEditor.Handles.DrawWireDisc(Owner.transform.position, Owner.transform.up, hearingRadius.Value);
            UnityEditor.Handles.color = oldColor;
#endif
        }

        public override void OnBehaviorComplete() {
            MovementUtility.ClearCache();
        }
        */
    }
}