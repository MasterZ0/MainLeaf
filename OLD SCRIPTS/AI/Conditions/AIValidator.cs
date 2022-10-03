using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace AI {
    [TaskCategory("AI")]
    public class AIValidator : Conditional {
        [RequiredField]
        public SharedAIController aiController;

        private NavMeshAgent navMeshAgent;

        public override void OnAwake() {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        public override TaskStatus OnUpdate() {
            if (aiController.Value.Alive) {
                return TaskStatus.Running;
            }

            navMeshAgent.isStopped = true;
            return TaskStatus.Failure;            
        }
    }
}