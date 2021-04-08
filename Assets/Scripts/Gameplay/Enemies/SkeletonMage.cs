using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMage : Enemy {
    [Header("Skeleton Mage")]
    private Transform player;

    [Header("Prefab")]
    [SerializeField] private PooledObject fireball;
    public override void AwakeEnemy() {
    }

    private void Start() {
        player = GameController.Player;
    }


    // Update is called once per frame
    void Update()
    {

    }
    public override void EnemyDeath() {
    }
}
