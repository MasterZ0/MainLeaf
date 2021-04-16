using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(FieldOfView))]
[RequireComponent(typeof(NavMesh))]
public class AIController : MonoBehaviour {

    [SerializeField] private FieldOfView fieldOfView;
    [SerializeField] public float timeStopped = 2f;

    public Transform target { get; private set; }
    public NavMeshAgent navMeshAgent { get; private set; }
    public Vector3 Velocity { get => navMeshAgent.velocity; }
    public EnemyAttributes enemyAttributes { get; private set; } 

    public bool IsChasing { get; set; }
    public event Action OnAttack;


    public void Init(EnemyAttributes attributes) {
        navMeshAgent = GetComponent<NavMeshAgent>();

        enemyAttributes = attributes;
        navMeshAgent.speed = enemyAttributes.walkSpeed;
    }
    public void Attack() {
        OnAttack.Invoke();
    }

    public bool TargetClose() {
        Physics.CheckSphere(transform.position, enemyAttributes.chaseRange, Constants.Layer.PLAYER);
        throw new NotImplementedException();
    }
    public bool HasTarget() {
        // target está vivo?

        return false;
    }
}