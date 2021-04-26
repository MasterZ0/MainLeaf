using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    [TaskCategory("AI")]
    public class WithinDistance : Conditional {

        [Header("Settings")]
        public float radius;
        public Vector3 offset;

        [Header("Search")]
        public bool search;
        public LayerMask targetMask;

        [Header("Config")]
        [RequiredField]
        public SharedGameObject targetObject;

        [Header("Gizmos")]
        public Color gizmos;

        private float sqrMagnitude;

        public override void OnAwake() {
            sqrMagnitude = radius * radius;
        }

        public override TaskStatus OnUpdate() {

            if (search) {
                Collider[] colliders = Physics.OverlapSphere(transform.position + offset, radius, targetMask);
                if (colliders.Length > 0) {
                    targetObject.Value = colliders[0].gameObject;
                    return TaskStatus.Success;
                }
            }
            else {
                Vector3 targetPos = targetObject.Value.transform.position;
                if (Vector3.SqrMagnitude(targetPos - (transform.position + offset)) < sqrMagnitude) {// Inside of the Chase Radius
                    return TaskStatus.Success;
                }
            }

            return TaskStatus.Failure;
        }
        public override void OnReset() {
            targetObject = null;
            radius = 50f;
            offset = Vector3.zero;
            gizmos = Color.gray;
        }

        // Draw the chase radius
        public override void OnDrawGizmos() {
            if (Owner == null) {
                return;
            }
            UnityEditor.Handles.color = gizmos;
            UnityEditor.Handles.DrawWireDisc(Owner.transform.position + offset, Owner.transform.up, radius);
        }
    }
}