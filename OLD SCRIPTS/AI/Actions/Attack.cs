
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    [TaskCategory("AI")]
    public class Attack : Action {  // Verifica se está dentro do raio
        [Header("Settings")]
        public float viewAngle = 180;
        public float viewDistance = 10;
        public Vector3 offset;

        [Header("Config")]
        [RequiredField]
        public SharedAIController aiController;
        [RequiredField]
        public SharedGameObject targetObject;

        private bool isAttacking;
        private bool attackDone;
        private float sqrMagnitude;
        public override void OnStart() {
            sqrMagnitude = viewDistance * viewDistance;
        }

        public override TaskStatus OnUpdate() {
            if (transform == null)
                return TaskStatus.Failure;

            if (isAttacking) {    // Está atacando?
                if (attackDone) {
                    isAttacking = false;
                    return TaskStatus.Success;
                }
                return TaskStatus.Running ;
            }


            Vector3 targetPos = targetObject.Value.transform.position;
            Vector3 direction = targetPos - transform.position + offset;
            if (Vector3.SqrMagnitude(direction) < sqrMagnitude) {
                if (Vector3.Angle(transform.forward, direction.normalized) < viewAngle / 2) {
                    attackDone = false;
                    isAttacking = true;
                    aiController.Value.StartAttack(AttackDone);
                    return TaskStatus.Running;
                }
            }

            return TaskStatus.Failure;
        }

        private void AttackDone() {
            attackDone = true;
        }

        public override void OnReset() {
            viewAngle = 180;
            viewDistance = 2.5f;
            offset = Vector3.zero;
        }

#if UNITY_EDITOR
        public override void OnDrawGizmos() {
            UnityEditor.Handles.color = Color.red;
            DrawLineOfSight(Owner.transform, offset, viewAngle, viewDistance);
        }
        public void DrawLineOfSight(Transform transform, Vector3 positionOffset, float fieldOfViewAngle, float viewDistance) {
            UnityEditor.Handles.color = new Color(1, 0, 0, .02f);
            var halfFOV = fieldOfViewAngle * 0.5f;
            var beginDirection = Quaternion.AngleAxis(-halfFOV, transform.up) * transform.forward;
            UnityEditor.Handles.DrawSolidArc(transform.TransformPoint(positionOffset), transform.up, beginDirection, fieldOfViewAngle, viewDistance);
        }
#endif

        public override void OnBehaviorComplete() {
            MovementUtility.ClearCache();
        }
    }
}
