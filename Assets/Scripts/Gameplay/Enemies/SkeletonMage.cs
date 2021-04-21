using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SkeletonMage : Enemy {
    [Header("Skeleton Mage")]
    [SerializeField] private AIController aiController;
    [SerializeField] private Animator animator;
    [SerializeField] private SkinnedMeshRenderer eyes;
    [SerializeField] private Transform[] firePoint;

    [Header(" - Prefab")]
    [SerializeField] private PooledObject fireball;

    private int attack;
    private Action onAttackEnd;
    protected override void Awake() {
        base.Awake();
        aiController.Init(this);
        aiController.OnUpdateTarget += OnUpdateTarget;    // Estado de anim = armed
        aiController.OnStartAttack += OnAttack;           // Estado de anim = attack
        onAttackEnd = aiController.OnAttackSuccess;       // Callback ao finalizar o ataque
        OnTakeDamage += aiController.OnTakeDamage;        // Evento ao receber dano (Detectar inimigo)
    }

    private void OnUpdateTarget(Transform newTarget) {
        animator.SetBool(Constants.Anim.IS_ARMED, newTarget);
    }

    protected override void OnEnablePooledObject() {
        base.OnEnablePooledObject();
        eyes.material.color = Color.white;
    }
    private void FixedUpdate() {
        float moveSpeed = enemyAttributes.sprintSpeed / aiController.Velocity.magnitude;
        animator.SetFloat(Constants.Anim.MOVE_SPEED, moveSpeed);
    }
    private void OnAttack() {
        attack = UnityEngine.Random.Range(0, 2);
        animator.SetInteger(Constants.Anim.ATTACK, attack);
    }
    public void OnSpawnFireball() {
        animator.SetInteger(Constants.Anim.ATTACK, -1);
        fireball.SpawnObject(firePoint[attack].position, firePoint[attack].rotation);
    }
    public void OnAttackEnd() {
        onAttackEnd.Invoke();
    }
    protected override void EnemyDeath() {
        //aiMovement.StopAllCoroutines();
        animator.SetTrigger(Constants.Anim.DEATH);
        eyes.material.color = Color.clear;
    }



}
