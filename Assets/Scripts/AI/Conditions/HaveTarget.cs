
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Movement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI {
    [TaskCategory("AI")]
    public class HaveTarget : Conditional {
        [RequiredField]
        public SharedGameObject targetObject;
        [RequiredField]
        public SharedAIController aiController;

        private NavMeshAgent navMeshAgent;

        public override void OnAwake() {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        public override TaskStatus OnUpdate() {
            if (transform == null || targetObject.Value == null)
                return TaskStatus.Failure;

            navMeshAgent.isStopped = false;
            aiController.Value.SetTarget(targetObject.Value);
            return TaskStatus.Success;
        }
    }
}
