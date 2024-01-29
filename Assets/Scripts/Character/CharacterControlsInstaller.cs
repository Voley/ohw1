using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp {

    public class CharacterControlsInstaller : MonoBehaviour {
        
        [SerializeField] CharacterController _characterController;
        [SerializeField] MoveComponent _characterMoveComponent;
        [SerializeField] InputManager _inputManager;

        private void OnEnable()
        {
            _inputManager.OnFireTriggered += _characterController.EnqueueFire;
            _inputManager.OnInputChanged += _characterMoveComponent.MoveByHorizontalDirection;
        }

        private void OnDisable()
        {
            _inputManager.OnFireTriggered -= _characterController.EnqueueFire;
            _inputManager.OnInputChanged -= _characterMoveComponent.MoveByHorizontalDirection;
        }
    }
}
