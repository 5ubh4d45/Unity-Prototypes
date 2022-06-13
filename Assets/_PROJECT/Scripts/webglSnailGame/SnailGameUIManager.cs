using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using WebGLSnailGame;

namespace WebGLSnailGame
{
    public class SnailGameUIManager : MonoBehaviour
    {


        public static Vector2 InputDirectionVector2 = Vector2.zero;
        private static KeyDirection _inputDirection;
        public static event Action<KeyDirection> OnDirectionInput; 

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            SetInput();
        }

        private void SetInput()
        {
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                // Debug.Log("UP");
                InputDirectionVector2.y = 1;
                _inputDirection = KeyDirection.Up;
                OnDirectionInput?.Invoke(_inputDirection);
            }
            else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                // Debug.Log("Down");
                InputDirectionVector2.y = -1;
                _inputDirection = KeyDirection.Down;
                OnDirectionInput?.Invoke(_inputDirection);

            }
            else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                // Debug.Log("left");
                InputDirectionVector2.x = -1;
                _inputDirection = KeyDirection.Left;
                OnDirectionInput?.Invoke(_inputDirection);

            }
            else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                // Debug.Log("right");
                InputDirectionVector2.x = 1;
                _inputDirection = KeyDirection.Right;
                OnDirectionInput?.Invoke(_inputDirection);
            }
        }
    }
}
