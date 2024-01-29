using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp {

    public class BulletSpawner : MonoBehaviour {
        [SerializeField] private Transform _disabledBulletsContainer;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private Bullet _bulletPrefab;

        private readonly Queue<Bullet> _bulletPool = new();

        internal void SpawnInitialBullets(int count)
        {
            for (var i = 0; i < count; i++) {
                var bullet = Instantiate(_bulletPrefab, _disabledBulletsContainer);
                _bulletPool.Enqueue(bullet);
            }
        }

        public Bullet RequestBullet()
        {
            Bullet result = null;

            if (_bulletPool.TryDequeue(out var bullet)) {
                result = bullet;
            }

            if (result == null) {
                result = Instantiate(_bulletPrefab);
            }

            result.transform.SetParent(_worldTransform);

            return result;
        }

        public void ReturnBullet(Bullet bullet)
        {
            bullet.transform.SetParent(_disabledBulletsContainer);
            _bulletPool.Enqueue(bullet);
        }
    }
}