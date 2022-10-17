using AdventureGame.ObjectPooling;
using System;
using UnityEngine;

namespace AdventureGame.AI
{
    /// <summary>
    /// Used for enemies that are spawned and sent to the pool. This component will send the enemy to the Pool after call the Kill Enemy node.
    /// </summary>
    public class TemporaryEnemy : MonoBehaviour
    {
        public event Action<TemporaryEnemy> OnKillEnemy;
        public Enemy Self { get; private set; }

        private void Awake()
        {
            Self = GetComponentInChildren<Enemy>(true);

            if (!Self)
                Debug.LogError($"Missing enemy component in Game Object: {gameObject}");

            name = $"[Temporary Enemy] {name}";
        }

        private void OnEnable()
        {
            Self.OnFinishEnemyDeath += OnFinishEnemyDeath;
            Self.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            Self.OnFinishEnemyDeath -= OnFinishEnemyDeath;
        }

        private void OnFinishEnemyDeath()
        {
            OnKillEnemy(this);
            transform.ReturnToPool();
        }
    }
}