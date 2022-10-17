using AdventureGame.AI;
using AdventureGame.Data;
using AdventureGame.Effects;
using AdventureGame.ObjectPooling;
using UnityEngine;

namespace AdventureGame.Gameplay
{

    public class EnemyGenerator : MonoBehaviour
    {
        [SerializeField] private Transform player; // temp
        [SerializeField] private SpawnSmoke spawSmoke;
        [SerializeField] private Transform[] spawPoints;

        private ArenaSettings Settings => GameSettings.Arena;
        private Transform[] Enemies => Settings.Enemies;

        private float time;

        private void FixedUpdate()
        {
            if (transform.childCount >= Settings.MaxEnemies)
                return;

            time += Time.fixedDeltaTime;
            if (time >= Settings.SpawFrequency)
            {
                time -= Settings.SpawFrequency;

                Vector3 position = GetRandomPosition();
                SpawnSmoke smoke = spawSmoke.SpawnPooledObject(position, Quaternion.identity, transform);
                smoke.Init(OnSpaw, Settings.SmokeWarmingDuration, Settings.SmokeDisappearsDuration, Settings.SmokeDelayToDestroy);
            }
        }

        private Vector3 GetRandomPosition()
        {
            int angle = Random.Range(0, 360);
            float area = Random.Range(0, Settings.SpawRadius);
            Vector3 offset = new Vector3()
            {
                x = area * Mathf.Cos(angle * Mathf.Deg2Rad),
                y = 0f,
                z = area * Mathf.Sin(angle * Mathf.Deg2Rad)
            };

            int index = Random.Range(0, spawPoints.Length);

            return spawPoints[index].position + offset;
        }

        public void OnSpaw(Vector3 position)
        {
            int random = Random.Range(0, Enemies.Length);

            Transform enemy = Enemies[random].SpawnPooledObject(position, Quaternion.identity, transform);
            enemy.transform.LookAt(player);

            TemporaryEnemy temporaryEnemy = enemy.GetComponent<TemporaryEnemy>();
            if (!temporaryEnemy)
            {
                temporaryEnemy = enemy.gameObject.AddComponent<TemporaryEnemy>();
            }

            // Add list? When game ends kill all
            temporaryEnemy.OnKillEnemy += OnEnemyDeath;
        }

        public void OnEnemyDeath(TemporaryEnemy temporaryEnemy)
        {
            temporaryEnemy.OnKillEnemy -= OnEnemyDeath;

            // Score?
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            foreach (Transform sp in spawPoints)
            {
                Gizmos.DrawWireSphere(sp.position, Settings.SpawRadius);
            }
        }
    }
}