using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        public Vector2 Position
        {
            get { return firePoint.position; }
        }

        public Quaternion Rotation
        {
            get { return firePoint.rotation; }
        }

        [SerializeField]
        private Transform firePoint;
    }
}