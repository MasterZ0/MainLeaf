using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarrior : Enemy {
    [Header("Skeleton Warrior")]
    [SerializeField] private Animator animator;
    [SerializeField] private AIController aiMovement;
    [SerializeField] private MeshCollider sword;
    [SerializeField] private SkinnedMeshRenderer eyes;
    //protected override void Awake() {
    //    base.Awake();
    //}
    protected override void OnEnablePooledObject() {
        base.OnEnablePooledObject();
    }
    private void Attack() {
        // Attack
        print("Atck");
        sword.enabled = true;
        return;
        animator.SetBool(Constants.Anim.ATTACK, true);
        aiMovement.TargetIsClose();
        sword.enabled = false;
    }
    protected override void EnemyDeath() {
        print("Is Dead");
        animator.SetTrigger(Constants.Anim.DEATH);
        eyes.material.color = Color.clear;
    }

    private void FixedUpdate() {
        UpdateAnimations();
    }

    private void UpdateAnimations() {
        aiMovement.StopAllCoroutines();
        animator.SetFloat(Constants.Anim.VELOCITY_Y, aiMovement.Velocity.z);
    }
}
