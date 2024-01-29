using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject _character; 
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private BulletSystem _bulletSystem;
        
        public bool _enqueueFire;

        public void EnqueueFire()
        {
            _enqueueFire = true;
        }

        private void OnEnable()
        {
            _character.GetComponent<HitPointsComponent>().OnHpEmpty += OnCharacterDeath;
        }

        private void OnDisable()
        {
            if (_character != null) {
                _character.GetComponent<HitPointsComponent>().OnHpEmpty -= OnCharacterDeath;
            }
        }

        private void OnCharacterDeath(GameObject _) => _gameManager.FinishGame();

        private void FixedUpdate()
        {
            if (_enqueueFire)
            {
                Fire();
                _enqueueFire = false;
            }
        }

        private void Fire()
        {
            var weapon = _character.GetComponent<WeaponComponent>();
            _bulletSystem.FireBulletOfType(BulletType.Player, weapon.transform, Vector2.zero);
        }
    }
}