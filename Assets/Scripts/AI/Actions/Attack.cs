
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
        public SharedFloat fieldOfViewAngle = 180;
        public SharedFloat viewDistance = 10;
        public SharedVector3 offset;

        [Header("Config")]
        [RequiredField]
        public SharedAIController aiController;
        [RequiredField]
        public SharedGameObject targetObject;
        [RequiredField]
        public SharedBool isAttacking;      //boleana de retorno

        [Header("Use Raycast")]
        public bool checkObstacles;      //boleana de retorno
        public LayerMask obscleLayer;      //boleana de retorno

        private bool attackDone;
        private float sqrMagnitude;
        public override void OnStart() {
            sqrMagnitude = viewDistance.Value * viewDistance.Value;
        }

        public override TaskStatus OnUpdate() {
            if (transform == null)
                return TaskStatus.Failure;

            if (isAttacking.Value) {    // Está atacando?
                if (attackDone) {
                    isAttacking.Value = false;
                    return TaskStatus.Success;
                }
                return TaskStatus.Running ;
            }
                


            Vector3 direction = (transform.position - targetObject.Value.transform.position);
            if (Vector3.SqrMagnitude(direction) < sqrMagnitude) {

                float angle = Vector3.Dot(transform.forward, direction.normalized).Remap(-1, 1, 0, 360);
                if (angle < viewDistance.Value) {
                    attackDone = false;
                    isAttacking.Value = true;
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
            fieldOfViewAngle = 180;
            viewDistance = 2.5f;
            offset = Vector3.zero;
        }

        public override void OnDrawGizmos() {
            UnityEditor.Handles.color = Color.red;
            DrawLineOfSight(Owner.transform, offset.Value, fieldOfViewAngle.Value, viewDistance.Value);
        }
        public void DrawLineOfSight(Transform transform, Vector3 positionOffset, float fieldOfViewAngle, float viewDistance) {
#if UNITY_EDITOR
            UnityEditor.Handles.color = new Color(1, 0, 0, .02f);
            var halfFOV = fieldOfViewAngle * 0.5f;
            var beginDirection = Quaternion.AngleAxis(-halfFOV, transform.up) * transform.forward;
            UnityEditor.Handles.DrawSolidArc(transform.TransformPoint(positionOffset), transform.up, beginDirection, fieldOfViewAngle, viewDistance);
#endif
        }

        public override void OnBehaviorComplete() {
            MovementUtility.ClearCache();
        }
    }
}
