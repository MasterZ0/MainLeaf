using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(FieldOfView))]
public class AIMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private FieldOfView fieldOfView;

    private Transform target;

    [SerializeField] private EnemyData enemyData;

    private const float walkPointRange = 5f;
    private const float timeToUpdate = .2f;

    private const float timeStopped = 2f;


    public void Init(Action arrivalCallback) {

        navMeshAgent.speed = enemyData.moveSpeed;
        print(Constants.Layer.PLAYER);
    }
    public void Patrol(Action findedTarget) {
        IEnumerator walkAround = Patrol();
        StartCoroutine(walkAround);

        fieldOfView.FindTarget((newTarget) => { 
            target = newTarget;
            StopCoroutine(walkAround);
            findedTarget();
        });
    }
    private IEnumerator Patrol() {
        while (true) {
            float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
            float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

            Vector3 walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
            navMeshAgent.SetDestination(walkPoint);

            yield return new WaitUntil(() => ArrivalCheck(.1f, walkPoint));
            yield return new WaitForSeconds(timeStopped);
        }
    }

    public void ChaseTarget(Action successful, Action failed) {

        navMeshAgent.SetDestination(target.position); // Move?
        StartCoroutine(ChaseTarget(() => ArrivalCheck(enemyData.attackRange, target)));
    }

    IEnumerator ChaseTarget(Func<bool> checkMethod) {
        yield return new WaitUntil(checkMethod);
    }

    public void CheckDistance() {
        Physics.CheckSphere(transform.position, enemyData.chaseRange, Constants.Layer.PLAYER);
        float k = enemyData.chaseRange;
    }

    public float CheckTargetDistance() {
        throw new NotImplementedException();
    }

    public void RunAway() {
        // Find Oposite Point
        // Find Random point
    }
    public void FindPosition() {

    }


    private bool ArrivalCheck(float minimunDistance, Vector3 target) {
        print("Hi");
        if (Vector3.Distance(transform.position, target) < minimunDistance) {
            return true;
        }
        return false;
    }
    private bool ArrivalCheck(float minimunDistance, Transform target) {
        if (Vector3.Distance(transform.position, target.position) < minimunDistance) {
            return true;
        }
        return false;
    }
}
