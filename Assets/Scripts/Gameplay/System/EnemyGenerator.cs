using System;
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
    [SerializeField] private Transform player;
    [SerializeField] private SpawnSmoke spawSmoke;
    [SerializeField] private Enemy[] enemies;
    [SerializeField] private Transform[] spawPoints;

    private NativeArray<float3> spawPointsPos;
    private float time = 1;
    private bool spawEnemies;
    private void Start() {
        GameController.OnChangeState += OnChangeState;
        spawPointsPos = new NativeArray<float3>(spawPoints.Length, Allocator.Persistent);
        for (int i = 0; i < spawPoints.Length; i++) {
            spawPointsPos[i] = spawPoints[i].position;
        }
    }

    private void OnChangeState(GameState gameState) {
        spawEnemies = gameState == GameState.Playing;
        if(gameState == GameState.Win) {
            // Kill Everybody
        }
    }
    

    private void Update() {
        if (!spawEnemies)
            return;

        time -= Time.deltaTime;
        if(time <= 0) {
            time = spawFrequency;

            SpawEnemie();
            //https://docs.unity3d.com/2021.1/Documentation/ScriptReference/Unity.Jobs.IJob.html
        }
    }

    struct VelocityJob : IJob {
        // Jobs declare all data that will be accessed in the job
        // By declaring it as read only, multiple jobs are allowed to access the data in parallel
        [ReadOnly]
        public NativeArray<Vector3> velocity;

        // By default containers are assumed to be read & write
        public NativeArray<Vector3> position;

        // Delta time must be copied to the job since jobs generally don't have concept of a frame.
        // The main thread waits for the job on the same frame or the next frame, but the job should
        // perform work in a deterministic and independent way when running on worker threads.
        public float deltaTime;

        // The code actually running on the job
        public void Execute() {
            // Move the positions based on delta time and velocity
            for (var i = 0; i < position.Length; i++)
                position[i] = position[i] + velocity[i] * deltaTime;
        }
    }

    public void Update2() {
        var position = new NativeArray<Vector3>(500, Allocator.Persistent);

        var velocity = new NativeArray<Vector3>(500, Allocator.Persistent);
        for (var i = 0; i < velocity.Length; i++)
            velocity[i] = new Vector3(0, 10, 0);


        // Initialize the job data
        var job = new VelocityJob()
        {
            deltaTime = Time.deltaTime,
            position = position,
            velocity = velocity
        };

        // Schedule the job, returns the JobHandle which can be waited upon later on
        JobHandle jobHandle = job.Schedule();

        // Ensure the job has completed
        // It is not recommended to Complete a job immediately,
        // since that gives you no actual parallelism.
        // You optimally want to schedule a job early in a frame and then wait for it later in the frame.
        jobHandle.Complete();

        Debug.Log(job.position[0]);

        // Native arrays must be disposed manually
        position.Dispose();
        velocity.Dispose();
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

        print(spawPointsPos[0] + " " + findPosition.result);
        //<<<< MELHORAR METHODO
        SpawnSmoke smoke = spawSmoke.SpawnObject(findPosition.result, Quaternion.identity).GetComponent<SpawnSmoke>();  
        smoke.Callback = OnSpaw;

        spawPointsPos.Dispose();
        // NativeArray Dispose?
    }

    public void OnSpaw(Vector3 position) {
        int random = UnityEngine.Random.Range(0, enemies.Length);
        PooledObject enemy = enemies[random].SpawnObject(position, Quaternion.identity);
        enemy.transform.LookAt(player);
    }

    [BurstCompile]
    public struct FindPosition : IJob {
        public NativeArray<float3> position;

        [ReadOnly]
        public float3 player;
        public float3 result;
        public float radius;
        public void Execute() {
            result = position[0];
            //Determinar qual ponto escolher, baseado nos paramentros e posição do playerss

            float angle = new Unity.Mathematics.Random(1).NextFloat(0, 360);
            float area = new Unity.Mathematics.Random(1).NextFloat(0, radius);
            result.x += area * math.cos(angle * Mathf.Deg2Rad);
            result.z += area * math.sin(angle * Mathf.Deg2Rad);
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        foreach (Transform sp in spawPoints) {
            Gizmos.DrawWireSphere(sp.position, spawRadius);
        }
    }
}
