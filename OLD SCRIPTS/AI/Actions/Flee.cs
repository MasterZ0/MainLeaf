using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AI {

    [TaskCategory("AI")]
    public class Flee : NavMeshMovement {
        [Header("Flee")]
        public float fleedDistance = 20;
        public float lookAheadDistance = 5;

        [Header(" - Config")]
        [RequiredField]
        public SharedGameObject target;
        public override void OnStart() {
            base.OnStart();

            SetDestination(Destination());
        }

        public override TaskStatus OnUpdate() {
            if (Vector3.Magnitude(transform.position - target.Value.transform.position) > fleedDistance) {
                return TaskStatus.Success;
            }

            if (HasArrived()) {
                SetDestination(Destination());
            }
            else if (agent.velocity.sqrMagnitude < moveSpeed.Value * .2f) { // Se estiver abaixo de 20% é porque algo esta errado
                // TODO: Se o caminho estiver ruim, arrumar outro destino
                return TaskStatus.Failure;
            }

            return TaskStatus.Running;
        }

        private Vector3 Destination() {
            // TODO: Checar melhor fulga
            return transform.position + (transform.position - target.Value.transform.position).normalized * lookAheadDistance;
        }

        public override void OnReset() {
            base.OnReset();

            fleedDistance = 20;
            lookAheadDistance = 5;
            target = null;
        }
    }
}