using AdventureGame.ObjectPooling;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

// Responsável por guardar os dados base e receber dano
public abstract class Enemy : MonoBehaviour/*, IDamageable*/ {


    public enum EnemyState
    {
        Begin,
        Patrolling,     // Patrulhar
        Chasing,        // Corre atrás do player
        Attacking,      // Attack
        RunAway,        // Mago - corre do player
        FindPosition,   // Mago - tenta encontra posição
        Dead,           // Inimigo Morreu
        Victory,        // Player morreu
    }

    [Header("Enemy Config")]
    [SerializeField] protected EnemyAttributes enemyAttributes;
    [SerializeField] protected SkinnedMeshRenderer[] skinnedMeshRenderers;

    [Header(" - Prefabs")]
    [SerializeField] protected MonoBehaviour deathFx;
    [SerializeField] protected MonoBehaviour disappearFx;

    public EnemyAttributes EnemyAttributes { get => enemyAttributes; }
    public bool IsDead { get; private set; }

    private Material[] defaultMaterials;
    private Material hitMaterial;

    private int currentLife;

    //public event Action<Damage> OnTakeDamage = delegate { };

    protected virtual void Awake() {
        //hitMaterial = Resources.Load<Material>(Constants.Path.HIT);

        defaultMaterials = new Material[skinnedMeshRenderers.Length];
        for (int i = 0; i < skinnedMeshRenderers.Length; i++) {
            defaultMaterials[i] = skinnedMeshRenderers[i].material;
        }
    }

    private void OnEnable()
    {
        currentLife = enemyAttributes.maxLife;
        IsDead = false;
    }

    //public virtual bool TakeDamage(Damage damage) {
    //    if (IsDead)
    //        return false;

    //    OnTakeDamage.Invoke(damage);
    //    currentLife -= damage.value;
    //    if(currentLife <= 0) {
    //        KillEnemy();
    //        return true;
    //    }
    //    StartCoroutine(HitMaterial());
    //    return false;
    //}
    public void KillEnemy() {
        IsDead = true;
        //deathFx.SpawnObject(transform.position, transform.rotation);
        GameController.EnemyDeath(this);
        EnemyDeath();
        StartCoroutine(DestroyEnemy());
    }
    protected abstract void EnemyDeath();   // Animação de morte, desabilitar rb, coroutines, etc...    
   
    private IEnumerator HitMaterial() {
        for (int i = 0; i < skinnedMeshRenderers.Length; i++) {
            skinnedMeshRenderers[i].material = hitMaterial;
        }
        yield return new WaitForSeconds(.1f);

        for (int i = 0; i < skinnedMeshRenderers.Length; i++) {
            skinnedMeshRenderers[i].material = defaultMaterials[i];
        }
    }
    private IEnumerator DestroyEnemy() {
        yield return new WaitForSeconds(2);
        SpawnLoot(transform.position, enemyAttributes.loots);
        //disappearFx.SpawnObject(transform.position, transform.rotation);
        //ReturnToPool();
    }

    private void SpawnLoot(Vector3 spawnPosition, Loot[] loots)
    {

        foreach (Loot loot in loots)
        {
            float luck = Random.Range(0, 100f);
            if (luck <= loot.chance)
                loot.item.SpawnPooledObject(spawnPosition, Quaternion.identity);
        }
    }
}