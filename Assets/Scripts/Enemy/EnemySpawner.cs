using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace ShootEmUp {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private Transform _disabledContainer;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private GameObject _enemyPrefab;

        private readonly Queue<GameObject> _pool = new();

        internal void SpawnInitialEnemies(int count)
        {
            for (var i = 0; i < count; i++) {
                var enemy = Instantiate(_enemyPrefab, _disabledContainer);
                _pool.Enqueue(enemy);
            }
        }

        public GameObject RequestEnemy()
        {
            GameObject result = null;

            if (_pool.TryDequeue(out var enemy)) {
                result = enemy;
            }

            if (result == null) {
                result = Instantiate(_enemyPrefab);
            }

            result.transform.SetParent(_worldTransform);

            var spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;

            var attackPosition = _enemyPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);

            return result;
        }

        public void ReturnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(_disabledContainer);
            _pool.Enqueue(enemy);
        }
    }
}