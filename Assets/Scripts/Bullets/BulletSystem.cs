using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public enum BulletType
    {
        Player,
        Enemy
    }

    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField]
        private int _initialBulletCount = 50;
        [SerializeField] private LevelBounds _levelBounds;
        [SerializeField] private BulletSpawner _spawner;

        [SerializeField] private BulletConfig _enemyBulletConfig;
        [SerializeField] private BulletConfig _playerBulletConfig;

        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> _bulletCache = new();
        
        private void Awake()
        {
            _spawner = FindObjectOfType<BulletSpawner>();
            _spawner.SpawnInitialBullets(_initialBulletCount);
        }
        
        private void FixedUpdate()
        {
            _bulletCache.Clear();
            _bulletCache.AddRange(_activeBullets);

            for (int i = 0, count = _bulletCache.Count; i < count; i++)
            {
                var bullet = _bulletCache[i];

                if (!_levelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }

        public void ShootBullet(BulletModel model)
        {
            var bullet = _spawner.RequestBullet();
            bullet.UpdateWithModel(model);
            
            if (_activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += OnBulletCollision;
            }
        }
        
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
                _spawner.ReturnBullet(bullet);
            }
        }

        public void FireBulletOfType(BulletType type, Transform source, Vector2 direction)
        {
            switch (type) {
                case BulletType.Player:
                    BulletModel playerBulletModel = new() {
                        isPlayer = true,
                        physicsLayer = (int)_playerBulletConfig.physicsLayer,
                        color = _playerBulletConfig.color,
                        damage = _playerBulletConfig.damage,
                        position = source.transform.position,
                        velocity = source.rotation * Vector3.up * _playerBulletConfig.speed
                    };
                    ShootBullet(playerBulletModel);
                    break;
                case BulletType.Enemy:
                    BulletModel enemyBulletModel = new() {
                        isPlayer = false,
                        physicsLayer = (int)_enemyBulletConfig.physicsLayer,
                        color = _enemyBulletConfig.color,
                        damage = _enemyBulletConfig.damage,
                        position = source.transform.position,
                        velocity = direction * _enemyBulletConfig.speed
                    };
                    ShootBullet(enemyBulletModel);
                    break;
            }
        }
    }
}