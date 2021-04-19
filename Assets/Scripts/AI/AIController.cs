using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {


    [SerializeField] private NavMeshAgent navMeshAgent;


    private EnemyAttributes enemyAttributes;    
    public event Action OnAttack;
    public Vector3 Velocity { get => navMeshAgent.velocity; }
    public Transform Target { get; private set; }
    internal GameObject ReicevedDamage() {
        return null;
    }

    public void Init(Enemy enemy) {
        //enemy.OnTakeDamage += OnTakeDamage;
        enemyAttributes = enemy.EnemyAttributes;
        navMeshAgent.speed = enemyAttributes.walkSpeed;
    }

    public void AttackStarted() {    // Attack
        OnAttack.Invoke();
    }

    public void FootR() {

    }
    public void FootL() { }

    //private void OnTakeDamage() {
    //}


}