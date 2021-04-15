using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SkeletonMage : Enemy {
    [Header("Skeleton Mage")]

    [Header(" - Config")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private AIController aiMovement;
    [SerializeField] private Animator animator;
    [SerializeField] private SkinnedMeshRenderer eyes;

    [Header(" - Prefab")]
    [SerializeField] private PooledObject fireball;


    private Transform target;
    private void Start() {
        target = GameController.Player;
    }
    protected override void OnEnable() {
        base.OnEnable();
    }
    protected override void OnEnablePooledObject() {
        base.OnEnablePooledObject();
    }
    

    private void Attack() {
        firePoint.LookAt(target);
        fireball.SpawObject(firePoint.position, firePoint.rotation);
    }
    protected override void EnemyDeath() {
        animator.SetTrigger(Constants.Anim.DEATH);
        eyes.material.color = Color.clear;
    }

    
}
