using Newtonsoft.Json.Linq;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField]
        private new Rigidbody2D _rigidbody2D;

        [SerializeField]
        private float _speed = 5.0f;
        
        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            var nextPosition = _rigidbody2D.position + vector * _speed;
            _rigidbody2D.MovePosition(nextPosition);
        }

        public void MoveByHorizontalDirection(float direction)
        {
            MoveByRigidbodyVelocity(new Vector2(direction, 0) * Time.fixedDeltaTime);
        }
    }
}