using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace AI {
    public abstract class NavMeshMovement : Action {
        [Header("Nav Mesh Movement")]
        public SharedFloat moveSpeed = 10;
        //public SharedFloat angularSpeed = 120;
        public SharedFloat stoppingDistance = 0.2f;
        [UnityEngine.Tooltip("Margin of error to find the path")]
        public SharedFloat maxDistanceHit = .5f;

        [Header(" - Config")]
        [RequiredField]
        public SharedNavMeshAgent navMeshAgent;

        protected NavMeshPath path;
        protected NavMeshAgent agent;

        public override void OnAwake() {
            path = new NavMeshPath();

            if (navMeshAgent == null)
                agent = GetComponent<NavMeshAgent>();
            else
                agent = navMeshAgent.Value;
        }

        public override void OnStart() {
            agent.speed = moveSpeed.Value;
            //agent.angularSpeed = angularSpeed.Value;
            agent.stoppingDistance = stoppingDistance.Value;
            agent.isStopped = false;
        }

        protected virtual bool SetDestination(Vector3 destination) {
            bool isValid = NavMesh.SamplePosition(destination, out NavMeshHit hit, maxDistanceHit.Value, NavMesh.AllAreas);
            if (isValid) {
                NavMesh.CalculatePath(transform.position, hit.position, NavMesh.AllAreas, path);
                agent.SetPath(path);
            }

            return isValid;
        }

        protected virtual bool HasPath() {
            return agent.hasPath && agent.remainingDistance > stoppingDistance.Value;
        }
        protected virtual bool HasArrived() {
            float remainingDistance;
            if (agent.pathPending) {
                remainingDistance = float.PositiveInfinity;
            }
            else {
                remainingDistance = agent.remainingDistance;
            }
            return remainingDistance <= agent.stoppingDistance;
        }

        public override void OnReset() {
            moveSpeed = 5;
            //angularSpeed = 120;
            maxDistanceHit = .5f;
            stoppingDistance = .2f;
        }
    }
}