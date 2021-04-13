using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(FieldOfView))]
public partial class AIMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private FieldOfView fieldOfView;

    public Vector3 Velocity { get => navMeshAgent.velocity; }

    [SerializeField] private EnemyData enemyData;

    private const float walkPointRange = 5f;
    private const float timeToUpdate = .2f;

    private const float timeStopped = 2f;


    public void Init(Action arrivalCallback) {

        navMeshAgent.speed = enemyData.moveSpeed;
        print(Constants.Layer.PLAYER);
    }
    public void Patrol(Action findedTarget) {
        IEnumerator walkAround = Patrol();
        StartCoroutine(walkAround);

        fieldOfView.FindTarget((newTarget) => { 
            target = newTarget;
            StopCoroutine(walkAround);
            findedTarget();
        });
    }
    private IEnumerator Patrol() {
        while (true) {
            float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
            float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

            Vector3 walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
            navMeshAgent.SetDestination(walkPoint);

            yield return new WaitUntil(() => ArrivalCheck(.1f, walkPoint));
            yield return new WaitForSeconds(timeStopped);
        }
    }

    public void ChaseTarget(Action successful, Action failed) {

        navMeshAgent.SetDestination(target.position); // Move?
        StartCoroutine(ChaseTarget(() => ArrivalCheck(enemyData.attackRange, target)));
    }

    IEnumerator ChaseTarget(Func<bool> checkMethod) {
        yield return new WaitUntil(checkMethod);
    }

    public void CheckDistance() {
        Physics.CheckSphere(transform.position, enemyData.chaseRange, Constants.Layer.PLAYER);
        float k = enemyData.chaseRange;
    }

    public float CheckTargetDistance() {
        throw new NotImplementedException();
    }

    public void RunAway() {
        // Find Oposite Point
        // Find Random point
    }
    public void FindPosition() {

    }


    private bool ArrivalCheck(float minimunDistance, Vector3 target) {
        if (Vector3.Distance(transform.position, target) < minimunDistance) {
            return true;
        }
        return false;
    }
    private bool ArrivalCheck(float minimunDistance, Transform target) {
        if (Vector3.Distance(transform.position, target.position) < minimunDistance) {
            return true;
        }
        return false;
    }





    public float radius = 1;


    [Range(1, 3)] // 1 = 8, 2 = 16, 3 = 32
    public float distance;
    public Transform target;
    public LayerMask whatIsObstacles;
    public List<Transform> obstacles;
    private float time;



    // andar em torno do spawpoint, e voltar gradualmente caso esteja saindo
    private void Start() {
        obstacles = new List<Transform>();
    }
    private void FixedUpdate() {
        time -= Time.fixedDeltaTime;
        if (time <= 0) {
            time = .2f;
            Collider[] cols = Physics.OverlapSphere(transform.position, radius, whatIsObstacles);

            obstacles.Clear();
            foreach (Collider col in cols) {
                obstacles.Add(col.transform);
            }
        }
    }

    public Vector3[] Intensity() {
        Vector3 targetDir = (target.position - transform.position).normalized;
        Vector3[] intencity = new Vector3[steps];


        for (int i = 0; i < steps; i++) {
            float targetDotProduct = Vector3.Dot(targetDir, rayDir[i]).Remap(-1, 1, 0, 1);
            intencity[i] += rayDir[i] * targetDotProduct;
        }
        GetImpedimentObstacles(ref intencity, targetDir); // ESPALHAR SOMENTE UMA VEZ
        return intencity;
    }

    private bool[] GetImpedimentObstacles(ref Vector3[] intencity, Vector3 targetDir) {
        bool[] blockedDirections = new bool[steps];
        foreach (Transform obstacle in obstacles) {

            Vector3 obstacleDir = (obstacle.position - transform.position).normalized;

            AddObstacleStrength(ref intencity, obstacleDir, targetDir);// Se o obstaculo estiver na mesma direção do alvo (.9375 sem remap)

        }
        return blockedDirections;
    }

    private void AddObstacleStrength(ref Vector3[] intencity, Vector3 obstacleDir, Vector3 targetDir) {    // Soma a intenciadade
        for (int i = 0; i < intencity.Length; i++) {

            float dotProduct = Vector3.Dot(obstacleDir, rayDir[i]).Remap(-1, 1, 0, 1);
            float spreadDot = 1f - Mathf.Abs(dotProduct - 0.65f);

            // Quanto mais próximo de 1 = .65
            float t = Vector3.Dot(obstacleDir, targetDir).Remap(-1, 1, 0, 1);
            float finalDot = (1 - t) * -dotProduct + t * (4 * spreadDot);

            intencity[i] += rayDir[i] * finalDot;
        }
    }
}

public partial class AIMovement {
    private const int steps = 16;
    private static Vector3[] RayDir { get; set; }
    private static Vector3[] rayDir {
        get {
            if (RayDir == null) {
                Vector3[] directions = new Vector3[steps];

                for (int i = 0; i < directions.Length; i++) {
                    float angle = 360f / steps * i;

                    directions[i].x = Mathf.Cos(angle * Mathf.Deg2Rad);
                    directions[i].z = Mathf.Sin(angle * Mathf.Deg2Rad);
                }
                RayDir = directions;
            }
            return RayDir;
        }
    }
}