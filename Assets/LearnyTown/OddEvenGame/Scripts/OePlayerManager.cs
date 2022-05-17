using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using Random = UnityEngine.Random;


namespace LearnyTown.OddEvenGame
{
    public class OePlayerManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreCard;
        [SerializeField] private TextMeshProUGUI _instructionCard;


        [SerializeField] private bool _isCurrentNoEven;
        // public static bool IsCurrentNoEven;

        private float _score;
        private string _instruction;
        
        private void OnEnable()
        {
            OeNumbers.OnNumberCollisionIsEven += CheckNumberCollision;
        }

        private void OnDisable()
        {
            OeNumbers.OnNumberCollisionIsEven -= CheckNumberCollision;
        }

        void Start()
        {
            SetInstruction();
        }

        void Update()
        {
        
        }

        private void CheckNumberCollision(bool isEven)
        {
            if (_isCurrentNoEven == isEven)
            {
                Debug.Log("RightAnswer");
                SetScore(100);
                SetInstruction();
            }else
            {
                Debug.Log("WrongAnswer");
                SetScore(-100);
                SetInstruction();
            }
        }

        private void SetScore(int scoresToAdd)
        {
            _score += scoresToAdd;
            _scoreCard.SetText($"SCORE: {_score}");
        }
        private void SetInstruction()
        {
            _isCurrentNoEven = 0 == Random.Range(0, 2);
            if (_isCurrentNoEven)
            {
                _instruction = "EVEN";
                _instructionCard.SetText(_instruction);
            }
            else
            {
                _instruction = "ODD";
                _instructionCard.SetText(_instruction);
                
            }
        }

        private void UpdateStatus()
        {
            // IsCurrentNoEven = _isCurrentNoEven;
        }
    }
}
