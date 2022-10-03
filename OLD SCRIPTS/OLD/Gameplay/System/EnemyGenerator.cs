using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
//using Unity.Burst;
//using Unity.Collections;
//using Unity.Jobs;
//using Unity.Mathematics;
public class EnemyGenerator : MonoBehaviour {
    [Header("Enemy Generator")]
    [SerializeField] [Min(.5f)] private float spawFrequency;
    [SerializeField] private float spawRadius;
    [SerializeField] private float maxEnemies;

    [SerializeField] [Range(40, 250)] private float minimumSpawnDistanceFromPlayer = 100f;
    [SerializeField] [Range(0, 1)] private float chanceToSpawnClosePlayer;

    [Header(" - Config ")]
    [SerializeField] private Transform player;
    //[SerializeField] private SpawnSmoke spawSmoke;
    [SerializeField] private Enemy[] enemies;
    [SerializeField] private Transform[] spawPoints;

    private Vector3[] spawPointsPos;
    private float time = 1;
    private bool spawEnemies;
    private List<Enemy> enemiesSpawned = new List<Enemy>();
    public static List<Transform> SpawPoints { get; private set; }

    private void Awake() {
        SpawPoints = new List<Transform>(spawPoints);
        GameController.OnChangeState += OnChangeState;
        GameController.OnEnemyDeath += OnEnemyDeath;
    }

    private void Start() {
        spawPointsPos = new Vector3[spawPoints.Length];
        for (int i = 0; i < spawPoints.Length; i++) {
            spawPointsPos[i] = spawPoints[i].position;
        }
    }

    private void OnChangeState(GameState gameState) {
        spawEnemies = gameState == GameState.Playing;
        if (gameState == GameState.Win) {
            foreach (Enemy enemy in enemiesSpawned) {
                enemy.KillEnemy();
            }
        }
    }


    private void Update() {
        if (!spawEnemies)
            return;

        time -= Time.deltaTime;
        if (time <= 0) {
            time = spawFrequency;

            if(enemiesSpawned.Count < maxEnemies)
                SpawEnemie();
        }
    }

    private void SpawEnemie() {
        //SpawnSmoke smoke = spawSmoke.SpawnObject(GetRandomPosition(), Quaternion.identity).GetComponent<SpawnSmoke>();
        //smoke.Callback = OnSpaw;
    }

    private Vector3 GetRandomPosition() {
        int index = Random.Range(0, spawPointsPos.Length);
        Vector3 result = spawPointsPos[index];

        int angle = Random.Range(0, 360);
        float area = Random.Range(0, spawRadius);

        result.x += area * Mathf.Cos(angle * Mathf.Deg2Rad);
        result.z += area * Mathf.Sin(angle * Mathf.Deg2Rad);
        return result;
    }

    public void OnSpaw(Vector3 position) {
        int random = Random.Range(0, enemies.Length);
        //Enemy enemy = enemies[random].SpawnObject(position, Quaternion.identity).GetComponent<Enemy>();
        //enemy.transform.LookAt(player);
        //enemiesSpawned.Add(enemy);
    }

    public void OnEnemyDeath(Enemy enemy) {
        if (enemiesSpawned.Contains(enemy)) {
            enemiesSpawned.Remove(enemy);
        }
        else {
            Debug.LogError($"Inimigo inexperado {enemy}");
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        foreach (Transform sp in spawPoints) {
            Gizmos.DrawWireSphere(sp.position, spawRadius);
        }
    }

}

/*      JOB SYSTEM
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
    [SerializeField] private GameObject[] spawPoints;

    private NativeArray<float3> spawPointsPos;
    private float time = 1;
    private bool spawEnemies;
    public static List<GameObject> SpawPoints { get; private set; }

    private void Awake() {
        SpawPoints = new List<GameObject>(spawPoints);
        GameController.OnChangeState += OnChangeState;
    }

    private void Start() {
        spawPointsPos = new NativeArray<float3>(spawPoints.Length, Allocator.Persistent);
        for (int i = 0; i < spawPoints.Length; i++) {
            spawPointsPos[i] = spawPoints[i].transform.position;
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
            spawPointsPos[i] = spawPoints[i].transform.position;
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
        foreach (GameObject sp in spawPoints) {
            Gizmos.DrawWireSphere(sp.transform.position, spawRadius);
        }
    }
}
*/