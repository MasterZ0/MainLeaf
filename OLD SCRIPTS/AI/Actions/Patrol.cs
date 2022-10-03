using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AI {

    [TaskCategory("AI")]
    public class Patrol : NavMeshMovement {
        [Header("Patrol")]
        public bool randomPatrol = false;
        [UnityEngine.Tooltip("The length of time that the agent should pause when arriving at a waypoint")]
        public float waypointPauseDuration = 0;

        [Header(" - Config")]
        [RequiredField]
        public SharedTransformList waypoints;

        private int waypointIndex;
        private float waypointReachedTime;   // Tempo alcançado

        public override void OnStart() {
            base.OnStart();

            if (randomPatrol) { // initially move towards the closest waypoint
                float distance = Mathf.Infinity;
                float localDistance;
                for (int i = 0; i < waypoints.Value.Count; ++i) {
                    localDistance = Vector3.Magnitude(transform.position - waypoints.Value[i].position);
                    if (localDistance < distance) {
                        distance = localDistance;
                        waypointIndex = i;
                    }
                }
            }
            else {
                waypointIndex = 0;
            }
            
            waypointReachedTime = -1;
            SetDestination(GetDestination());
        }

        public override TaskStatus OnUpdate() {
            if (waypoints.Value.Count == 0) {
                return TaskStatus.Failure;
            }

            if (HasArrived()) {
                if (waypointReachedTime == -1) {
                    waypointReachedTime = Time.time; 
                }

                // wait the required duration before switching waypoints.
                if (waypointReachedTime + waypointPauseDuration <= Time.time) {
                    SetDestination(GetDestination());
                    waypointReachedTime = -1;
                }
            }

            return TaskStatus.Running;
        }

        private Vector3 GetDestination() {
            if (randomPatrol) 
                waypointIndex = waypointIndex.NewRandomIndex(waypoints.Value.Count);            
            else
                waypointIndex = waypointIndex.Navigate(waypoints.Value.Count, true);

            return waypoints.Value[waypointIndex].position;
        }

        public override void OnReset() {
            base.OnReset();

            randomPatrol = false;
            waypointPauseDuration = 0;
            waypoints = null;
        }

//        public override void OnDrawGizmos() {
//#if UNITY_EDITOR
//            if (waypoints == null || waypoints.Value == null) {
//                return;
//            }
//            var oldColor = UnityEditor.Handles.color;
//            UnityEditor.Handles.color = Color.yellow;
//            for (int i = 0; i < waypoints.Value.Count; ++i) {
//                if (waypoints.Value[i] != null) {
//                    UnityEditor.Handles.SphereHandleCap(0, waypoints.Value[i].transform.position, waypoints.Value[i].transform.rotation, 1, EventType.Repaint);
//                }
//            }
//            UnityEditor.Handles.color = oldColor;
//#endif
//        }
    }
}