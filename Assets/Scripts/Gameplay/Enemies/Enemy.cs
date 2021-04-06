using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    [Header("Enemy")]
    [SerializeField] protected EnemyData enemyData;
    [SerializeField] protected Material[] defaultMaterials;


    private int currentLife;
    private Material hitMaterial;
    private void Awake() {
        currentLife = enemyData.life;
        //defaultMaterial = GetComponent<MeshRenderer>().material;
        hitMaterial = Resources.Load<Material>(Constants.Path.HIT);
        AwakeEnemy();
    }

    public virtual void TakeDamage(int damage) {
        currentLife -= damage;
        if(currentLife <= 0) {
            HUD.Instance.AddPoints(enemyData.points);
        }
    }
    public abstract void AwakeEnemy();
    public abstract void EnemyDeath();
}
