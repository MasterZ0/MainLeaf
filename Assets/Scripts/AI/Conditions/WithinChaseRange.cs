using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AI {
    [TaskCategory("AI")]
    public class WithinChaseRange : Conditional {

        [Header("Settings")]
        public SharedFloat magnitude = 5;
        public SharedVector3 offset;

        [Header("Config")]
        [RequiredField]
        public SharedGameObject targetObject;
        [RequiredField]
        public SharedAIController aiController;
        [RequiredField]
        public SharedBool isChasing;

        private float sqrMagnitude;
        private Vector3 targetPosition;

        public override void OnStart() {    // WITHIN
            sqrMagnitude = magnitude.Value * magnitude.Value;
            targetPosition = targetObject.Value.transform.position;
        }

        public override TaskStatus OnUpdate() {
            if (transform == null)
                return TaskStatus.Failure;

            Vector3 direction = targetPosition - (transform.position + offset.Value);
            if (Vector3.SqrMagnitude(direction) < sqrMagnitude) {
                return TaskStatus.Success;
            }

            aiController.Value.SetTarget(null);
            return TaskStatus.Failure;
        }
        public override void OnReset() {
            targetObject = null;
            magnitude = 5;
            offset = Vector3.zero;
        }

        // Draw the seeing radius
        public override void OnDrawGizmos() {
#if UNITY_EDITOR
            if (Owner == null) {
                return;
            }
            //var oldColor = UnityEditor.Handles.color;
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(Owner.transform.position + offset.Value, Owner.transform.up, magnitude.Value);
            //UnityEditor.Handles.color = oldColor;
#endif
        }
    }

}