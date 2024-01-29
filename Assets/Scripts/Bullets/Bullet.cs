using System;
using System.Drawing;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        [NonSerialized] public bool isPlayer;
        [NonSerialized] public int damage;

        [SerializeField]
        private new Rigidbody2D _rigidbody2D;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }

        public void UpdateWithModel(BulletModel model)
        {
            transform.position = model.position;
            _spriteRenderer.color = model.color;
            gameObject.layer = model.physicsLayer;
            damage = model.damage;
            isPlayer = model.isPlayer;
            _rigidbody2D.velocity = model.velocity;
        }
    }
}