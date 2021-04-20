using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    [TaskCategory("AI")]
    public class TargetIsAlive : Conditional {

        [RequiredField]
        public SharedGameObject targetObject;
        [RequiredField]
        public SharedAIController aiController;

        private IDamageable targetStatus;
        private GameObject currentTarget;
        public override void OnStart() { 
            if (currentTarget != targetObject.Value) {
                currentTarget = targetObject.Value;
                targetStatus = currentTarget.GetComponent<IDamageable>();
            }
        }

        public override TaskStatus OnUpdate() {
            if (currentTarget == null || targetStatus.IsDead) {
                aiController.Value.SetTarget(null);
                return TaskStatus.Failure;
            }
            return TaskStatus.Success;
        }
    }
}