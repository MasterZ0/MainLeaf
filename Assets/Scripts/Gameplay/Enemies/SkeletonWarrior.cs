using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarrior : Enemy {
    [Header("Skeleton Warrior")]
    [SerializeField] private Animator animator;

    private Transform player;
    private float time = .2f;
    private const float updateFrequency = .2f;
    public override void AwakeEnemy() {
    }
    void Start() {
        player = GameController.Player;
    }
    public override void EnemyDeath() {
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        time -= Time.fixedDeltaTime;
        if(time <= 0) {
            time = updateFrequency;
            navMeshAgent.SetDestination(player.position);
        }
    }
}
