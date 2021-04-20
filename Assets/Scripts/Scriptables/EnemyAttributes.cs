using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Scriptable Objects/Enemy Data", order = 0)]
public class EnemyAttributes : ScriptableObject {
    public int maxLife;
    public float walkSpeed;
    public float sprintSpeed;

    public float attackRange;
    public int points;
    public Loot[] loots;

    [HideInInspector]
    public float chaseRange = 50f;
    
    [System.Serializable]
    public struct Loot {
        public Item item;    //item type = Ammon, gold, life
        [Range(0, 100)]
        public float chance;
    }

    public void SpawnLoot(Vector3 spawnPosition) {

        foreach (Loot loot in loots) {
            float luck = Random.Range(0, 100f);
            if(luck <= loot.chance)
                loot.item.SpawnObject(spawnPosition, Quaternion.identity);
        }
    }
}

