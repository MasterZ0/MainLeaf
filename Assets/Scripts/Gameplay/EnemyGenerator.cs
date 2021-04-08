using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {
    [Header("Enemy Generator")]
    [SerializeField] [Min(.5f)] private float spawFrequency;
    [SerializeField] private float spawRadius;

    [SerializeField] [Range(40, 250)] private float minimumSpawnDistanceFromPlayer = 100f;
    [SerializeField] [Range(0, 1)] private float chanceToSpawnClosePlayer;

    [Header(" - Config ")]
    [SerializeField] private PooledObject spawEffect;
    [SerializeField] private Enemy[] enemies;
    [SerializeField] private Transform[] spawPoints;

    private Transform player;
    private NativeArray<float3> spawPointsPos;
    private float time = 1;
    private void Start() {
        player = GameController.Player;
        spawPointsPos = new NativeArray<float3>(spawPoints.Length, Allocator.Persistent);
        for (int i = 0; i < spawPoints.Length; i++) {
            spawPointsPos[i] = spawPoints[i].position;
        }
    }

    private void Update() {
        time -= Time.deltaTime;
        if(time <= 0) {
            time = spawFrequency;

            SpawEnemie();
        }
    }

    private void SpawEnemie() {

        NativeArray<float3> spawPointsPos = new NativeArray<float3>(spawPoints.Length, Allocator.TempJob);
        for (int i = 0; i < spawPoints.Length; i++) {
            spawPointsPos[i] = spawPoints[i].position;
        }

        FindPosition findPosition = new FindPosition
        {
            player = player.position,
            position = spawPointsPos
        };
        JobHandle jobHandle = findPosition.Schedule();
        jobHandle.Complete();

        int r = UnityEngine.Random.Range(0, enemies.Length);
        PooledObject enemy = enemies[r].SpawObject(findPosition.result, Quaternion.identity);
        enemy.transform.LookAt(player);

        print(spawPointsPos[1] + " " + findPosition.result);
        spawPointsPos.Dispose();
        // NativeArray Dispose?
    }

    [BurstCompile]
    public struct FindPosition : IJob {
        public NativeArray<float3> position;
        public float3 player;
        public float3 result;

        public void Execute() {
            result = position[1];
            result.x += 1;
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        foreach (Transform sp in spawPoints) {
            Gizmos.DrawWireSphere(sp.position, spawRadius);
        }
    }
}
