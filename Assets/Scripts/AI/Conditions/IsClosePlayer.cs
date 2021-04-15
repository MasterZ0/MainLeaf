using BBUnity.Conditions;
using Pada1.BBCore;


namespace AI {
    [Condition("Game/Perception/IsClosePlayer")]
    public class IsClosePlayer : GOCondition {

        [InParam("AIController")]
        private AIController aiController;

        public override bool Check() {
            return aiController.TargetIsClose();
        }
    }
}

