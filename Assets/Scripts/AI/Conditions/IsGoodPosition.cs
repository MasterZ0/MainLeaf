using BBUnity.Conditions;
using Pada1.BBCore;

[Condition("Game/Perception/IsGoodPosition")]
public class IsGoodPosition : GOCondition {

    [InParam("AIController")]
    private AIController aiController;

    public override bool Check() {
        return aiController.TargetIsClose();
    }
}
