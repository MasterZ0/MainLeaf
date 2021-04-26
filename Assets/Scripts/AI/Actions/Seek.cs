using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

// https://learn.unity.com/tutorial/unity-navmesh
// https://youtu.be/blPglabGueM

namespace AI {
    [TaskDescription("Checks whether the target is within the Chase Radius, whether the target is alive, and return a valid position to the navmesh")]
    [TaskCategory("AI")]
    public class Seek : NavMeshMovement {

        [Header("Seek")]
        [RequiredField]
        public SharedGameObject target;

        private Vector3 oldTargetPos;

        public override TaskStatus OnUpdate() {
            Vector3 newTargetPos = target.Value.transform.position;
            if (oldTargetPos.x != newTargetPos.x || oldTargetPos.z != newTargetPos.z) { // Alvo se mexeu
                oldTargetPos = newTargetPos;

                SetDestination(newTargetPos);
            }

            if (HasArrived()) {
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }

        //public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        //    Vector3 randDirection = Random.insideUnitSphere * dist + origin;
        //    NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, layermask);
        //    return navHit.position;
        //}
    }
}