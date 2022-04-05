using System;
using System.Collections;
using System.Collections.Generic;
using FidgetMathGame;
using TMPro;
using UnityEngine;

namespace FidgetMathGame
{


    public class MathUIController : MonoBehaviour
    {
        [Tooltip("Without the '='")]
        [SerializeField] private string _question;

        [SerializeField] private int _answer;

        [SerializeField] private TextMeshProUGUI _questionText;
        [SerializeField] private TextMeshProUGUI _answerText;
        private void OnEnable()
        {
            FidgetBall.BallTapped += CheckAnswer;
        }

        private void OnDisable()
        {
            FidgetBall.BallTapped -= CheckAnswer;
        }

        // Start is called before the first frame update
        void Start()
        {
            _questionText.text = _question;
            _answerText.text = null;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void CheckAnswer(int number)
        {
            if(number != _answer) return;
            _answerText.text = _answer.ToString();
            Debug.Log("You Won!");
        }
    }
}