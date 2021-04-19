using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace AI {
    [TaskCategory("AI")]
    public class ReceivedDamage : Conditional {

        [RequiredField]
        public SharedAIController aiController;

        [RequiredField]
        public SharedGameObject returnedObject;

        public override TaskStatus OnUpdate() {
            returnedObject.Value = aiController.Value.ReicevedDamage();
            return returnedObject.Value != null ? TaskStatus.Success : TaskStatus.Failure;
        }

        //public override bool Check() {
        //    return aiController.TargetClose();
        //}
    }
}

