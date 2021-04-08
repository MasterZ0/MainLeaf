using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Scriptable Objects/Enemy Data", order = 0)]
public class EnemyData : ScriptableObject
{
    public int life;
    public float moveSpeed;

    public float attackDistance;
    public int points;
}
