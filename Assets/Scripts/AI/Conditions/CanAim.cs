
using BehaviorDesigner.Runtime.Tasks;

namespace AI {
    [TaskCategory("AI")]
    public class CanAim : Conditional {
        public SharedAIController aiController;
        public override void OnStart() {
            base.OnStart();
        }
        public override TaskStatus OnUpdate() {
            return TaskStatus.Failure;
        }
    }

}
