using UnityEngine;
using System;
using System.Collections.Generic;
using DG.Tweening;
using LearnyTown.MathRacingGame;

namespace LearnyTown.MathRacingGame
{
    public class MrCarsController : MonoBehaviour
    {

        [SerializeField] private MrCar _playerCar;
        [SerializeField] private MrCar _aiCar;

        [Space]
        [SerializeField] private float _aiCarStepSize;
        [SerializeField] private float _aiCarSteppingTime;
        
        [Space]
        [SerializeField] private int _gameOverScore;
        [SerializeField] private int _aiCarLastPos;
        
        
        private bool _isGameOver;

        private int _wrongAnsNo;
        private int _currentAnsCount;
        private float _aiCarCurrentZPos;


        private void OnEnable()
        {
            MrUiManager.OnAnswer += SetAnswer;
        }
        private void OnDisable()
        {
            MrUiManager.OnAnswer -= SetAnswer;
        }

        // Start is called before the first frame update
        void Start()
        {
            _aiCarCurrentZPos = _aiCar.CarTransform.position.z;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void SetAnswer(int ans)
        {
            _wrongAnsNo += ans;
            _wrongAnsNo = Mathf.Clamp(_wrongAnsNo, _aiCarLastPos, _gameOverScore);
            MoveCar(_aiCar);
        }

        private void MoveCar(MrCar targetCar)
        {
            var distance = (_wrongAnsNo - _currentAnsCount) * _aiCarStepSize;

            targetCar.CarTransform.DOMoveZ(_aiCarCurrentZPos + distance, _aiCarSteppingTime).SetEase(Ease.OutExpo)
                .OnComplete(() =>
                {
                    _currentAnsCount = _wrongAnsNo;
                    _aiCarCurrentZPos = targetCar.CarTransform.position.z;
                    if (_wrongAnsNo >= _gameOverScore)
                    {
                        _isGameOver = true;
                    }
                });
        }
        
    }

    [Serializable]
    public class MrCar
    {
        public Transform CarTransform;
    }
}
