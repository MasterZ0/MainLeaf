using System;
using System.Collections;
using UnityEngine;

// Responsável por guardar os dados base e receber dano
public abstract class Enemy : PooledObject, IDamageable {
    [Header("Enemy Config")]
    [SerializeField] protected EnemyAttributes enemyData;
    [SerializeField] protected SkinnedMeshRenderer[] skinnedMeshRenderers;

    [Header(" - Prefabs")]
    [SerializeField] protected PooledObject deathFx;
    [SerializeField] protected PooledObject disappearFx;

    private Material[] defaultMaterials;
    private Material hitMaterial;

    private bool isDead;
    public EnemyState enemyState { get; protected set; }

    public bool IsDead => throw new NotImplementedException();

    private int currentLife;

    public event Action DeathEvent;

    protected virtual void Awake() {
        hitMaterial = Resources.Load<Material>(Constants.Path.HIT);

        defaultMaterials = new Material[skinnedMeshRenderers.Length];
        for (int i = 0; i < skinnedMeshRenderers.Length; i++) {
            defaultMaterials[i] = skinnedMeshRenderers[i].material;
        }
    }
    protected override void OnEnablePooledObject() {
        currentLife = enemyData.maxLife;
    }
    public virtual bool TakeDamage(int damage) {
        currentLife -= damage;
        StartCoroutine(HitMaterial());
        if(currentLife <= 0) {
            KillEnemy();
            return true;
        }
        return false;
    }
    protected abstract void EnemyDeath();   // Animação de morte, desabilitar rb, coroutines, etc...

    private void KillEnemy() {
        if (isDead)
            return;

        isDead = true;
        deathFx.SpawObject(transform.position, transform.rotation);
        HUD.Instance.AddPoints(enemyData.points);
        EnemyDeath();
        StartCoroutine(DestroyEnemy());
    }
    private IEnumerator DestroyEnemy() {
        yield return new WaitForSeconds(2);
        disappearFx.SpawObject(transform.position, transform.rotation);
        DesactivePooledObject();
    }
    private IEnumerator HitMaterial() {
        for (int i = 0; i < skinnedMeshRenderers.Length; i++) {
            skinnedMeshRenderers[i].material = hitMaterial;
        }
        yield return new WaitForSeconds(.1f);

        for (int i = 0; i < skinnedMeshRenderers.Length; i++) {
            skinnedMeshRenderers[i].material = defaultMaterials[i];
        }
    }
}
public enum EnemyState {
    Begin,
    Patrolling,     // Patrulhar
    Chasing,        // Corre atrás do player
    Attacking,      // Attack
    RunAway,        // Mago - corre do player
    FindPosition,   // Mago - tenta encontra posição
    Dead,           // Inimigo Morreu
    Victory,        // Player morreu
}