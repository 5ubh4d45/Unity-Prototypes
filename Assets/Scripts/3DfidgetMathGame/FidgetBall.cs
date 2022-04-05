using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.InputSystem;
using FidgetMathGame;
using CraftGame;

namespace FidgetMathGame
{
    public class FidgetBall : MonoBehaviour
    {
        public FidgetMathBall MathBall;
        [SerializeField] private BallController _ballController;

        public static event Action<int> BallTapped;

        private bool _canRotate;
        public bool CanRotate => _canRotate;
        
        // Start is called before the first frame update
        void Start()
        {
            _canRotate = true;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        
        public void RotateBall()
        {
            if (!_canRotate) return;
            MathBall.Ball.DORotate(_ballController.RotationTargetFwd, _ballController.rotationTime, RotateMode.Fast).SetEase(Ease.Linear);
            MathBall.TextUI.enabled = false;
            BallTapped?.Invoke(MathBall.Number);
            _canRotate = false;
        }
    }
    
    [System.Serializable]
    public class FidgetMathBall
    {
        public int Number;
        public Transform Ball;
        public TextMeshPro TextUI;
        
    }
}