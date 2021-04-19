
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    [TaskCategory("AI")]
    public class FindPosition : Action {
        [Header(" - Config")]
        [SerializeField] private float viewRadius;
        [SerializeField] private Transform eye;
        [SerializeField] private LayerMask targetMask;
        [SerializeField] private LayerMask obstacleMask;

        [SerializeField] private List<Transform> obstacles;
        public Transform Target { get; set; }


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

        public Vector3[] Intensity() {
            Vector3 targetDir = (Target.position - transform.position).normalized;
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
        private void CheckObstacles() {
            Collider[] cols = Physics.OverlapSphere(transform.position, viewRadius, obstacleMask);

            obstacles.Clear();
            foreach (Collider col in cols) {
                obstacles.Add(col.transform);
            }
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
}