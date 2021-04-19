
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
        public override TaskStatus OnUpdate() {
            if (transform == null || targetObject.Value == null)
                return TaskStatus.Failure;

            return TaskStatus.Success;
        }
    }
}
