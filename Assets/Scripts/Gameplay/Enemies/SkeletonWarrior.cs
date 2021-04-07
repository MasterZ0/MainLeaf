using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarrior : Enemy {
    [Header("Skeleton Warrior")]
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform target;
    public override void AwakeEnemy() {
    }

    public override void EnemyDeath() {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    float time = 2;
    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time <= 0) {
            time = 2;
            navMeshAgent.SetDestination(target.position);
        }
    }
}
