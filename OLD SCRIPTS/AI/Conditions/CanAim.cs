
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AI {
    [TaskCategory("AI")]
    public class CanAim : Conditional {
        [RequiredField]
        public SharedGameObject targetObject;

        public SharedVector3 offset;
        public LayerMask obstacleMask;      //boleana de retorno
        
        public override TaskStatus OnUpdate() {
            Vector3 targetPosition = targetObject.Value.transform.position;
            Vector3 targetDirection = (targetPosition - transform.position + offset.Value);
            float distToTarget = Vector3.Distance(transform.position + offset.Value, targetPosition);

            if (!Physics.Raycast(transform.position + offset.Value, targetDirection.normalized, distToTarget, obstacleMask)) { // Se não houver nenhum obstaculo
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }

}
