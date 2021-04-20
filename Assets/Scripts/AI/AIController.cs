using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
public class AIController : MonoBehaviour {

    [SerializeField] private NavMeshAgent navMeshAgent;
    public EnemyAttributes EnemyAttributes { get; private set; }
    public Vector3 Velocity { get => navMeshAgent.velocity; }

    public event Action OnStartAttack;
    public event Action<Transform> OnUpdateTarget;

    private Action OnAttackEnd;
    private Damage reicebedDamage;

    public void Init(Enemy enemy) { // Chame no Awake
        EnemyAttributes = enemy.EnemyAttributes;
    }

    public void SetTarget(GameObject target) {
        OnUpdateTarget.Invoke(target?.transform);
    }

    public void StartAttack(Action callback) {  // Attack -> TaskStatus = Running
        OnAttackEnd = callback;
        OnStartAttack.Invoke();
    }
    public void OnAttackSuccess() {             // Attack -> TaskStatus = Sucess
        OnAttackEnd.Invoke();   
    }
    public void OnTakeDamage(Damage damage) {   // Detecta a posição do inimigo
        StartCoroutine(TakeDamage(damage));
    }
    IEnumerator TakeDamage(Damage damage) {
        reicebedDamage = damage;
        yield return new WaitForSeconds(1);     // Tempo pra resetar
        reicebedDamage.sender = null;
    }
    public GameObject ReicevedDamage() {        // Get posição do inimigo
        return reicebedDamage.sender;
    }

    public void FootR() {

    }
    public void FootL() { }

    //private void OnTakeDamage() {
    //}


}