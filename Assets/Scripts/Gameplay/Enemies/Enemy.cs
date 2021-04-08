using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : PooledObject {

    [Header("Enemy")]
    [SerializeField] protected EnemyData enemyData;
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected Material[] defaultMaterials;

    

    private int currentLife;
    private Material hitMaterial;
    private void Awake() {
        navMeshAgent.speed = enemyData.moveSpeed;
        //defaultMaterial = GetComponent<MeshRenderer>().material;
        hitMaterial = Resources.Load<Material>(Constants.Path.HIT);
        AwakeEnemy();
    }
    protected override void StartObject() {
        currentLife = enemyData.life;
    }
    public virtual void TakeDamage(int damage) {
        currentLife -= damage;
        if(currentLife <= 0) {
            HUD.Instance.AddPoints(enemyData.points);
            EnemyDeath();
        }
    }
    public abstract void AwakeEnemy();
    public abstract void EnemyDeath();
}
