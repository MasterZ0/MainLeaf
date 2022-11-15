using AdventureGame.AI;
using AdventureGame.BattleSystem;
using AdventureGame.Data;
using AdventureGame.Effects;
using AdventureGame.ObjectPooling;
using AdventureGame.Shared.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AdventureGame.Gameplay
{
    public class EnemyGenerator : MonoBehaviour
    {
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private SpawnSmoke spawSmoke;
        [SerializeField] private Transform enemiesContainer;
        [SerializeField] private Transform[] spawPoints;

        public event Action OnEnemyDeath = delegate { };

        private readonly List<Enemy> enemies = new List<Enemy>();
        private float time;

        private ArenaSettings Settings => GameSettings.Arena;
        private Transform[] Enemies => Settings.Enemies;

        private void FixedUpdate()
        {
            if (enemiesContainer.childCount >= Settings.MaxEnemies)
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


        /// <summary> Called after smoke has warmed </summary>
        private void OnSpaw(Vector3 position)
        {
            if (!enabled)
                return;

            int random = Random.Range(0, Enemies.Length);

            Transform enemy = Enemies[random].SpawnPooledObject(position, Quaternion.identity, enemiesContainer);

            Collider[] col = Physics.OverlapSphere(position, Settings.SearchPlayerRadius, playerLayer);

            if (col.Length > 0)
            {
                enemy.transform.LookAtY(col[0].transform.position);
            }

            TemporaryEnemy temporaryEnemy = enemy.GetComponent<TemporaryEnemy>();
            if (!temporaryEnemy)
            {
                temporaryEnemy = enemy.gameObject.AddComponent<TemporaryEnemy>();
            }

            enemies.Add(temporaryEnemy.Enemy);

            // Add list? When game ends kill all
            temporaryEnemy.OnRemoveEnemy += OnRemoveEnemy;
            temporaryEnemy.OnEnemyDie += OnEnemyDie;
        }

        private void OnRemoveEnemy(TemporaryEnemy temporaryEnemy)
        {
            temporaryEnemy.OnRemoveEnemy -= OnRemoveEnemy;
            temporaryEnemy.OnEnemyDie -= OnEnemyDie;
        }

        private void OnEnemyDie(Enemy enemy)
        {
            enemies.Remove(enemy);
            OnEnemyDeath();
        }

        public void KillAll()
        {
            enemies.ToList().ForEach(e => e.ForceKill(true));
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