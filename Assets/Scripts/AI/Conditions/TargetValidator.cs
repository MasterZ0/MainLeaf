using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI {
    [TaskDescription("Checks whether the target is within the Chase Radius and if the target is alive")]
    [TaskCategory("AI")]
    public class TargetValidator : Conditional {

        [Header("Settings")]
        public float chaseRadius;
        public Vector3 offset;

        [Header("Config")]
        [RequiredField]
        public SharedGameObject targetObject;
        [RequiredField]
        public SharedAIController aiController;

        private float sqrMagnitude;

        public override void OnAwake() {
            sqrMagnitude = chaseRadius * chaseRadius;
        }

        public override TaskStatus OnUpdate() {
            if (!aiController.Value.TargetIsAlive)    // Target must be alive
                return TaskStatus.Failure;

            Vector3 targetPos = targetObject.Value.transform.position;
            if (Vector3.SqrMagnitude(targetPos - (transform.position + offset)) < sqrMagnitude)// Inside of the Chase Radius
                return TaskStatus.Success;

            aiController.Value.SetTarget(null);
            return TaskStatus.Failure;
        }
        public override void OnReset() {
            targetObject = null;
            chaseRadius = 50f;
            offset = Vector3.zero;
        }

        // Draw the chase radius
        public override void OnDrawGizmos() {
            if (Owner == null) {
                return;
            }
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(Owner.transform.position + offset, Owner.transform.up, chaseRadius);
        }
    }

}