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

    private Transform target;
    private Action onAttackEnd;
    protected override void Awake() {
        base.Awake();
        aiMovement.Init(this);
        aiMovement.OnUpdateTarget += OnUpdateTarget;    // Estado de anim = armed
        aiMovement.OnStartAttack += OnAttack;           // Estado de anim = attack
        onAttackEnd = aiMovement.OnAttackSuccess;       // Callback ao finalizar o ataque
        OnTakeDamage += aiMovement.OnTakeDamage;        // Evento ao receber dano (Detectar inimigo)
    }

    private void OnUpdateTarget(Transform newTarget) {
        target = newTarget;
        animator.SetBool(Constants.Anim.IS_ARMED, newTarget);
    }

    protected override void OnEnablePooledObject() {
        base.OnEnablePooledObject();
    }
    private void FixedUpdate() {
        float moveSpeed = 0;
        if (aiMovement.Velocity.magnitude > 0)
            moveSpeed = enemyAttributes.sprintSpeed / aiMovement.Velocity.magnitude;

        animator.SetFloat(Constants.Anim.MOVE_SPEED, moveSpeed);
    }
    private void OnAttack() {
        float dirX = (target.position - transform.position).normalized.x;
        int attack = (dirX < -.5f) ? 0 : (dirX > .5f) ? 2 : 1; // left 0, center 1, right 2
        animator.SetInteger(Constants.Anim.ATTACK, attack) ;
    }
    public void OnActiveSword() {
        sword.enabled = true;
    }
    public void OnDesativeSword() {
        onAttackEnd.Invoke();
        sword.enabled = true;
        animator.SetInteger(Constants.Anim.ATTACK, -1);
    }
    protected override void EnemyDeath() {
        aiMovement.StopAllCoroutines();
        sword.enabled = false;
        animator.SetTrigger(Constants.Anim.DEATH);
        eyes.material.color = Color.clear;
    }
}
