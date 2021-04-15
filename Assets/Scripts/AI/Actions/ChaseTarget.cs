using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Action("Game/ChaseTarget")]
public class ChaseTarget : BasePrimitiveAction {

    [InParam("Target")]
    private GameObject target;
    [InParam("AIController")]
    private AIController aiController;
    public override void OnStart() {
        base.OnStart();
        aiController.currentSpeed = aiController.enemyAttributes.sprintSpeed;
        aiController.isChasing = true;
    }

    public override void OnAbort() {
        base.OnAbort();
        aiController.isChasing = false;
    }

    public override TaskStatus OnUpdate() {
        Vector3 toTarget = target.transform.position - aiController.transform.position;
        
        return TaskStatus.RUNNING;
    }
}
