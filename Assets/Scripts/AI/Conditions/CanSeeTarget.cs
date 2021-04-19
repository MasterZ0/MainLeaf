using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using UnityEditor;
using UnityEngine;

//Não vou usar por enquanto
namespace AI {  
    [TaskCategory("AI")]
    public class CanSeeTarget : Conditional {
 

        public SharedGameObject returnedObject;
        private Vector3 offset;

        public LayerMask obstacleMask;
        public LayerMask targetMask;
        public float viewAngle;
        public float viewRadius;

        public override void OnStart() {
        }

        public override TaskStatus OnUpdate() {
            if (FindVisibleTargets()) {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
        public override void OnReset() {
            viewAngle = 90;
            viewRadius = 75;
        }
        private bool FindVisibleTargets() {
            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position + offset, viewRadius, targetMask);

            for (int i = 0; i < targetsInViewRadius.Length; i++) {
                Transform target = targetsInViewRadius[i].transform;

                Vector3 targetDirection = (target.position - transform.position + offset).normalized;
                if (Vector3.Angle(transform.forward, targetDirection) < viewAngle / 2) { // Está dentro do angulo de visão?
                    float distToTarget = Vector3.Distance(transform.position + offset, target.position);

                    if (!Physics.Raycast(transform.position + offset, targetDirection, distToTarget, obstacleMask)) { // Se não houver nenhum obstaculo
                        returnedObject.Value = target.gameObject;
                        return true;
                    }
                }
            }
            return false;
        }

        public override void OnDrawGizmos() {
            //Vector3 direction = Quaternion.Euler(0, -aiController.ViewAngle / 2, 0) * transform.forward;
            Vector3 direction = Quaternion.AngleAxis(-viewAngle / 2, Vector3.up) * Owner.transform.forward;
            Handles.color = new Color(1f, 1f, 0f, 0.002f);
            Handles.DrawSolidArc(Owner.transform.position + offset, Owner.transform.up, direction, viewAngle, viewRadius);
        }
    }
}
