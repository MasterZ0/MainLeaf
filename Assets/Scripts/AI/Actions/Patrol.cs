using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Action("Game/Patrol")]
public class Patrol : BasePrimitiveAction{
    [InParam("AIController")]
    private AIController aiController;
    public override void OnStart() {
        base.OnStart();
        aiController.StartCoroutine(Patrolling());
    }


    private const float walkPointRange = 5f;

    public override TaskStatus OnUpdate() {
        return TaskStatus.RUNNING;
    }

    public override void OnAbort() {
        base.OnAbort();
        //remover corotina
        aiController.StopAllCoroutines();
    }

    private IEnumerator Patrolling() {
        Transform transform = aiController.transform;
        while (true) {
            Vector3 walkPoint = transform.position;
            walkPoint.x += Random.Range(-walkPointRange, walkPointRange);
            walkPoint.z += Random.Range(-walkPointRange, walkPointRange);

            aiController.navMeshAgent.SetDestination(walkPoint);

            while (Vector3.Distance(transform.position, walkPoint) < .01f) {
                yield return new WaitForSeconds(.2f);
            }
            yield return new WaitForSeconds(aiController.timeStopped);
        }
    }

}
