using BBUnity.Conditions;
using Pada1.BBCore;

[Condition("Game/Perception/IsTargetFar")]
public class IsTargetFar : GOCondition {

    [InParam("AIController")]
    private AIController aiController;

    public override bool Check() {
        return aiController.TargetIsClose();
    }
}
