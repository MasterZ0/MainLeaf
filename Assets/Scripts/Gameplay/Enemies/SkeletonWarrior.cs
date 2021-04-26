using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarrior : Enemy {
    [Header("Skeleton Warrior")]
    [SerializeField] private Animator animator;
    [SerializeField] private AIController aiController;
    [SerializeField] private MeshCollider sword;
    [SerializeField] private SkinnedMeshRenderer eyes;

    private Transform target;
    protected override void Awake() {
        base.Awake();
        aiController.Init(this);
        aiController.OnUpdateTarget += OnUpdateTarget;    // Estado de anim = armed
        aiController.OnStartAttack += OnAttack;           // Estado de anim = attack
        OnTakeDamage += aiController.OnTakeDamage;        // Evento ao receber dano (Detectar inimigo)
    }
    protected override void OnEnablePooledObject() {
        base.OnEnablePooledObject();
        eyes.material.color = Color.white;
    }

    private void FixedUpdate() {
        float moveSpeed = 0;
        if (aiController.Velocity.magnitude > 0)
            moveSpeed = enemyAttributes.sprintSpeed / aiController.Velocity.magnitude;

        animator.SetFloat(Constants.Anim.MOVE_SPEED, moveSpeed);
    }
    private void OnUpdateTarget(Transform newTarget) {
        target = newTarget;
        animator.SetBool(Constants.Anim.IS_ARMED, newTarget);
    }
    private void OnAttack() {
        float dirX = transform.InverseTransformPoint(target.position).x;
        int attack = (dirX < -.5f) ? 0 : (dirX > .5f) ? 2 : 1; // left 0, center 1, right 2
        animator.SetInteger(Constants.Anim.ATTACK, attack) ;
    }
    public void OnActiveSword() {
        sword.enabled = true;
    }
    public void OnDesativeSword() {
        sword.enabled = false;
        animator.SetInteger(Constants.Anim.ATTACK, -1);
    }
    protected override void EnemyDeath() {
        //aiMovement.StopAllCoroutines();
        aiController.Die();
        sword.enabled = false;
        animator.SetTrigger(Constants.Anim.DEATH);
        eyes.material.color = Color.clear;
    }
}
