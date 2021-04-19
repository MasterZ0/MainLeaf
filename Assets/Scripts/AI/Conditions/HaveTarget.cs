
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Movement;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    [TaskCategory("AI")]
    public class HaveTarget : Conditional {
        [RequiredField]
        public SharedGameObject targetObject;
        [RequiredField]
        public SharedAIController aiController;
        public override TaskStatus OnUpdate() {
            if (transform == null || targetObject.Value == null)
                return TaskStatus.Failure;

            aiController.Value.SetTarget(targetObject.Value);
            return TaskStatus.Success;
        }
    }
}
