using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LearnyTown.FractionFootballGame
{
    public class FfGoalStrikerController : MonoBehaviour
    {
        [SerializeField] private Animator _strikerAnimator;
        [SerializeField] private FfBallController _ball;
        
        [Space]
        [SerializeField] private float _forceAmountZ;
        [SerializeField] private Vector2 _xLimit;
        [SerializeField] private Vector2 _yLimit;

        [Space]
        [SerializeField] private bool _canMissShot;
        [SerializeField] private float _xMiss;
        [SerializeField] private float _yMiss;


        private void OnEnable()
        {
            FfUiManager.OnCorrectAnswerShoot += KickBall;
            FfUiManager.OnResetAnswer += ResetBall;
        }

        private void OnDisable()
        {
            FfUiManager.OnCorrectAnswerShoot -= KickBall;
            FfUiManager.OnResetAnswer -= ResetBall;
        }

        [ContextMenu("Kick Ball")]
        internal void KickBall(bool isCorrectAns)
        {
            _canMissShot = !isCorrectAns;
            
            var xForce = Random.Range(_xLimit.x, _xLimit.y);
            var yForce = Random.Range(_yLimit.x, _yLimit.y);
            var zForce = _forceAmountZ;

            if (_canMissShot)
            {
                xForce = Random.Range(_xLimit.x - _xMiss, _xLimit.y + _xMiss);
                yForce = Random.Range(_yLimit.y, _yLimit.y + _yMiss);
            }
            
            var force = new Vector3(xForce, yForce, zForce);
            
            // _strikerAnimator.Play("Kick");
            _ball.KickBall(force);
            
        }

        [ContextMenu("Reset")]
        internal void ResetBall()
        {
            _ball.Reset();
        }
        
    }
}
