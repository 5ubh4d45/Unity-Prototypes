using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FidgetMathGame;
using UnityEngine.UI;
using CraftGame;
using Unity.VisualScripting;

namespace FidgetMathGame
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] public Vector3 RotationTargetFwd;
        [SerializeField] public float rotationTime = 1f;
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private Camera _mainCamera;
        
        [SerializeField] private List<FidgetBall> _fidgetMathBalls;

        public static bool CanTap;
        
        private Vector3 _rotationTargetRev;
        private Vector2 _pointerPositionScr;

        private bool _isTouchPressed;

        private void OnEnable()
        {
            InputManager.TouchPressed += OnTouchPressed;
            InputManager.TouchReleased += OnTouchReleased;
        }

        private void OnDisable()
        {
            InputManager.TouchPressed -= OnTouchPressed;
            InputManager.TouchReleased -= OnTouchReleased;
        }

        // Start is called before the first frame update
        void Start()
        {
            foreach (var ball in _fidgetMathBalls)
            {
                ball.MathBall.TextUI.text = ball.MathBall.Number.ToString();
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }


        private void OnTouchReleased()
        {
            _isTouchPressed = false;
        }
        private void OnTouchPressed()
        {
            _isTouchPressed = true;
            
            if(!CanTap) return;
            
            // Debug.Log("Clicked");
            if (_mainCamera == null)
            {
                _mainCamera = Camera.main;
                return;
            }
            
            // Debug.Log("After Cam Check");

            Ray ray = _mainCamera.ScreenPointToRay(_inputManager.GetTouchPostion());
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red); // debug ray
            
            // Debug.Log("After Ray Check");

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Debug.Log("After hit Check Pass");
                if (hit.collider != null && hit.collider.gameObject.GetComponentInParent<FidgetBall>() != null)
                {
                    // Debug.Log("Starting Ball Rotation");
                    SetFidgetBall(hit.collider.gameObject.transform.parent.gameObject);
                }
            }
            // Debug.Log("After hit Check Fail");

        }

        private void SetFidgetBall(GameObject clickedObject)
        {
            if (!_isTouchPressed) return;
            clickedObject.TryGetComponent<FidgetBall>(out var fidgetBall);
            // clickedObject.TryGetComponent<>()
            if(!fidgetBall.CanRotate) return;
            
            fidgetBall.RotateBall();
            
            Debug.Log("RotatedBall");
            
        }
        
        
    }
    
}
