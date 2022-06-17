using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace LearnyTown.FractionFootballGame
{
    public class FfGoalKeeperController : MonoBehaviour
    {

        [SerializeField] private Transform _goalKeeper;
        [SerializeField] private float _movementDuration;
        [SerializeField] private float _jumpRange;
        // [SerializeField] private float _moveAmplitude;
        // [SerializeField] private float _movePeriod;

        [Space] [SerializeField] private bool _canBlock;
        [SerializeField] private float _missDistance;
        // [SerializeField] private float _jumpPower;
        // [SerializeField] private float _jumpLimit;
        // [SerializeField] private AnimationCurve _movementCurve;
        
        
        private Vector3 _goalPos;
        private float _keeperPosZ;

        private void OnTriggerEnter(Collider other)
        {
            var ballPos = other.transform.position;
            _goalPos = transform.position;
            _keeperPosZ = _goalKeeper.position.z;

            // var ballPosY = Mathf.Clamp(ballPos.y, 0f, _jumpLimit);
            
            if (ballPos.x > _goalPos.x && ballPos.z < _goalPos.z)
            {
                // ball in right & front
                if (_canBlock)
                {
                    DoMoveRight(ballPos.x);
                }
                else
                {
                    // DoMoveLeft(-ballPos.x);
                    DoMoveRight(ballPos.x - _missDistance);
                }
            }
            else if(ballPos.x < _goalPos.x && ballPos.z < _goalPos.z)
            {
                // ball in left & front
                if (_canBlock)
                {
                    DoMoveLeft(ballPos.x);
                }
                else
                {
                    // DoMoveRight(-ballPos.x);
                    DoMoveLeft(ballPos.x + _missDistance);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var ballPos = other.transform.position;
            _goalPos = transform.position;
            _keeperPosZ = _goalKeeper.position.z;
            if (ballPos.z > _goalPos.z)
            {
                DoMoveCenter();
            }
        }

        // [ContextMenu("Move Left")]
        private void DoMoveLeft(float ballPosX, float ballPosY = 0f)
        {
            var moveDisLeft = Mathf.Clamp(ballPosX, ballPosX, -_jumpRange);
            
            // _goalKeeper.DOLocalJump(new Vector3(_goalKeeper.position.x + moveDisLeft, _goalKeeper.position.y + ballPosY, _goalKeeper.position.z), 
            //     _jumpPower, 1, _movementDuration)
            //     .SetEase(_movementCurve);
            _goalKeeper.DOLocalMoveX(moveDisLeft, _movementDuration).SetEase(Ease.OutExpo);
            // _goalKeeper.DOLocalMoveY(ballPosY, _movementDuration * 0.5f).SetLoops(1, LoopType.Yoyo);

        }
        
        // [ContextMenu("Move Right")]
        private void DoMoveRight(float ballPosX, float ballPosY = 0f)
        {
            var moveDisRight = Mathf.Clamp(ballPosX, ballPosX, _jumpRange);
            
            // _goalKeeper.DOLocalJump(new Vector3(_goalKeeper.position.x + moveDisRight, _goalKeeper.position.y + ballPosY, _goalKeeper.position.z), 
            //         _jumpPower, 1, _movementDuration)
            //     .SetEase(_movementCurve);
            _goalKeeper.DOLocalMoveX(moveDisRight, _movementDuration).SetEase(Ease.OutExpo);
            // _goalKeeper.DOLocalMoveY(ballPosY, _movementDuration * 0.5f).SetLoops(1, LoopType.Yoyo);
        }
        
        // [ContextMenu("Move Center")]
        private void DoMoveCenter()
        {
            _goalKeeper.DOLocalMoveX(0, _movementDuration).SetEase(Ease.OutExpo);
            
        }
    }
}

