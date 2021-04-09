using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SkeletonMage : Enemy {
    [Header("Skeleton Mage")]

    [Header(" - Config")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private AIMovement AIMovement;

    [Header(" - Prefab")]
    [SerializeField] private PooledObject fireball;


    private Transform target;

    protected override void ResetEnemy() {

    }
    private void Start() {
        target = GameController.Player;
    }

    protected void Attack() {
        firePoint.LookAt(target);
        fireball.SpawObject(firePoint.position, firePoint.rotation);
    }
    protected override void EnemyDeath() {
    }

    
}
