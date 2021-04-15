using BBUnity.Conditions;
using Pada1.BBCore;
using UnityEngine;

[Condition("Game/Perception/IsTargetVisible")]
public class IsTargetVisible : GOCondition {

    [InParam("AIController")]
    private FieldOfView fieldOfView;

    [InParam("TargetMemoryDuration")]
    private float targetMemoryDuration;
    private float forgetTargetTime;

    public override bool Check() {

        if (fieldOfView.HasTarget()) {
            forgetTargetTime = Time.time + targetMemoryDuration;
            return true;
        }
        return Time.time < forgetTargetTime;
    }
}
