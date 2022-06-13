using System;
using System.Collections.Generic;
using UnityEngine;

namespace LearnyTown.MissingNumberGame
{
    public class MnBridgeManager : MonoBehaviour
    {

        [SerializeField] private MnUIManager _uiManager;
        [SerializeField] private MnCarController _playerController;
        
        
        [Space]
        [SerializeField] private Transform _stopTargetTransform;

        [SerializeField] private float _bridgeSnappingTime; 

        internal static event Action<List<int>, int> OnUpdatedAns;

        private MnNumberBridge _currentBridge;
        internal List<int> currentAnsList;
        internal int correctAns;
        internal int correctLane;
        
        private float _inputWaitTime;


        private void OnEnable()
        {
            MnNumberBridge.OnPlayerArrival += OnPlayerArrival;
            MnNumberBridge.OnPlayerDeparture += OnPlayerDeparture;
            MnUIManager.OnAnswerResponse += AnswerResponse;
        }

        private void OnDisable()
        {
            MnNumberBridge.OnPlayerArrival += OnPlayerArrival;
            MnNumberBridge.OnPlayerDeparture += OnPlayerDeparture;
            MnUIManager.OnAnswerResponse += AnswerResponse;
        }
        
        private void OnPlayerArrival(MnNumberBridge bridge)
        {
            _currentBridge = bridge;
            OnUpdatedAns?.Invoke(_currentBridge.ansList, _currentBridge.correctAns);

            // StartCoroutine(_currentBridge.OpenBridge());
            StartCoroutine(_currentBridge.StopBridge(_stopTargetTransform.position, _inputWaitTime, _bridgeSnappingTime));
            
            _playerController.ChangeLane(_currentBridge.lanePosX);
        }
        private void OnPlayerDeparture(MnNumberBridge bridge)
        {
            // departed
            StartCoroutine(_currentBridge.ClosBridge());
        }
        private void AnswerResponse(bool correctResponse)
        {
            if (!correctResponse)
            {
                _currentBridge.isGameOver = true;
            }
            
            StartCoroutine(_currentBridge.OpenBridge());
            
        }

        void Start()
        {
            _inputWaitTime = _uiManager.InputWaitTime;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
