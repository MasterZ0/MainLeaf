using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AI {
    [TaskCategory("AI")]
    public class ReceivedDamage : Conditional {

        [RequiredField]
        public SharedAIController aiController;

        [RequiredField]
        public SharedGameObject returnedObject;

        public override TaskStatus OnUpdate() {
            Transform target = aiController.Value.ReicevedDamage();
            if(target == null) {
                return TaskStatus.Failure;
            }
            returnedObject.Value = target.gameObject;
            return TaskStatus.Success;
        }

        //public override bool Check() {
        //    return aiController.TargetClose();
        //}
    }
}

