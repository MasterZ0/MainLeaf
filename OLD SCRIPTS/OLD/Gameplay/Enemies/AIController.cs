using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
public class AIController : MonoBehaviour {

    [SerializeField] private NavMeshAgent navMeshAgent;
    public EnemyAttributes EnemyAttributes { get; private set; }
    public Vector3 Velocity { get => navMeshAgent.velocity; }   // desiredVelocity?
    //public bool TargetIsAlive { get => !targetDamageable.IsDead; }
    public bool Alive { get; private set; }

    public event Action OnStartAttack;
    public event Action<Transform> OnUpdateTarget;


    private const float damageResetTime = 2f;
    private Action onAttackEnd;
    //private Damage reicebedDamage;
    //private IDamageable targetDamageable;

    public void Init(Enemy enemy) { // Chame no Awake
        EnemyAttributes = enemy.EnemyAttributes;
        Alive = true;
    }

    public void SetTarget(GameObject target) {
        if (target) {
            //targetDamageable = GetComponent<IDamageable>();
        }
        OnUpdateTarget.Invoke(target?.transform);
    }

    public void StartAttack(Action callback) {  // Attack -> TaskStatus = Running
        onAttackEnd = callback;
        OnStartAttack.Invoke();
    }
    public void OnAttackEnd() {                 // Attack -> TaskStatus = Sucess
        onAttackEnd.Invoke();   
    }
    //public void OnTakeDamage(Damage damage) {   // Detecta a posição do inimigo
    //    StartCoroutine(TakeDamage(damage));
    //}
    //IEnumerator TakeDamage(Damage damage) {
    //    reicebedDamage = damage;
    //    yield return new WaitForSeconds(damageResetTime);     // Tempo pra resetar
    //    reicebedDamage.sender = null;
    //}
    //public Transform ReicevedDamage() {        // Get posição do inimigo
    //    return reicebedDamage.sender;
    //}

    public void FootR() {

    }
    public void FootL() { }

    public void Die() {
        Alive = false;
    }

    //private void OnTakeDamage() {
    //}


}