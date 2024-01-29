using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        public float HorizontalDirection { get; private set; }

        public Action OnFireTriggered;
        public Action<float> OnInputChanged;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnFireTriggered();
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                HorizontalDirection = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                HorizontalDirection = 1;
            }
            else
            {
                HorizontalDirection = 0;
            }
        }

        private void FixedUpdate()
        {
            OnInputChanged(HorizontalDirection);
        }
    }
}