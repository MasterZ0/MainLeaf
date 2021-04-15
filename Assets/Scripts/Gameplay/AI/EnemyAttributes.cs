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
    public Loot[] loot;

    [HideInInspector]
    public float chaseRange = 50f;
    
    public struct Loot {
        public GameObject loot;    //item type = Ammon, gold, life
        public float chance;
    }
}

