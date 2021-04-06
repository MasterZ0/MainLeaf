using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMage : Enemy {
    [Header("Skeleton Mage")]

    private Transform player;
    public override void AwakeEnemy() {
        throw new System.NotImplementedException();
    }

    private void Start() {
        player = GameController.Player;
    }


    // Update is called once per frame
    void Update()
    {

    }
    public override void EnemyDeath() {
        throw new System.NotImplementedException();
    }
}
