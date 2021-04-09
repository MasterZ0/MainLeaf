using System.Collections;
using UnityEngine;

// Responsável por guardar os dados base e receber dano
public abstract class Enemy : PooledObject {
    [Header("Enemy Config")]
    [SerializeField] protected EnemyData enemyData;
    [SerializeField] protected SkinnedMeshRenderer[] skinnedMeshRenderers;

    [Header(" - Prefabs")]
    [SerializeField] protected PooledObject deathFx;
    [SerializeField] protected PooledObject disappearFx;

    private Material[] defaultMaterials;
    private Material hitMaterial;

    public EnemyState enemyState { get; protected set; }    
    private int currentLife;

    private void Awake() => AwakeEnemy();
    protected virtual void AwakeEnemy() { // Metodo para proteger o Awake desta classe

        hitMaterial = Resources.Load<Material>(Constants.Path.HIT);

        defaultMaterials = new Material[skinnedMeshRenderers.Length];
        for (int i = 0; i < skinnedMeshRenderers.Length; i++) {
            defaultMaterials[i] = skinnedMeshRenderers[i].material;
        }
    }
    protected override void StartObject() {
        currentLife = enemyData.life;
        ResetEnemy();
    }
    protected abstract void ResetEnemy();   // Este metodo é chamado quando o inimigo renascer
    public virtual bool TakeDamage(int damage) {
        currentLife -= damage;
        StartCoroutine(HitMaterial());
        if(currentLife <= 0) {
            KillEnemy();
            return true;
        }
        return false;
    }

    private void KillEnemy() {
        deathFx.SpawObject(transform.position, transform.rotation);
        HUD.Instance.AddPoints(enemyData.points);
        EnemyDeath();
        StartCoroutine(DestroyEnemy());
    }
    protected abstract void EnemyDeath();   // Animação de morte, desabilitar rb, coroutines, etc...
    private IEnumerator DestroyEnemy() {
        disappearFx.SpawObject(transform.position, transform.rotation);
        yield return new WaitForSeconds(2);
        ReturnToPool();
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