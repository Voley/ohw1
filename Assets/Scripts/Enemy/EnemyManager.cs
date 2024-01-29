using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _enemySpawner;

        [SerializeField]
        private BulletSystem _bulletSystem;
        
        private readonly HashSet<GameObject> m_activeEnemies = new();

        private IEnumerator Start()
        {
            _enemySpawner.SpawnInitialEnemies(10);

            while (true)
            {
                yield return new WaitForSeconds(1);
                var enemy = _enemySpawner.RequestEnemy();
                if (enemy != null)
                {
                    if (m_activeEnemies.Add(enemy))
                    {
                        enemy.GetComponent<HitPointsComponent>().OnHpEmpty += OnDestroyed;
                        enemy.GetComponent<EnemyAttackAgent>().OnFire += OnFire;
                    }    
                }
            }
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (m_activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnHpEmpty -= OnDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnFire -= OnFire;

                _enemySpawner.ReturnEnemy(enemy);
            }
        }

        private void OnFire(GameObject enemy, Vector2 direction)
        {
            _bulletSystem.FireBulletOfType(BulletType.Enemy, enemy.transform, direction);
        }
    }
}