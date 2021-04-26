using BehaviorDesigner.Runtime;
using UnityEngine.AI;

namespace AI {
    [System.Serializable]
    public class SharedNavMeshAgent : SharedVariable<NavMeshAgent> {
        public static implicit operator SharedNavMeshAgent(NavMeshAgent value) { return new SharedNavMeshAgent { mValue = value }; }
    }
}
