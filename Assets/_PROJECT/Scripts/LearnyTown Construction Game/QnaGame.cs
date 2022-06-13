using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LearnyTown.ConstructionGame
{

    public class QnaGame : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _checkButton;
        [SerializeField] private List<string> _correctAnswers;

        private bool _givenCorrectAnswer;
        
        // Start is called before the first frame update
        void Start()
        {
            _checkButton.onClick.AddListener(delegate { CheckAnswer(_inputField.text); });
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void CheckAnswer(string givenAnswer)
        {
            _givenCorrectAnswer = false;
            var givenAnswerlower = givenAnswer.ToLower();
            foreach (var correctAnswer in _correctAnswers)
            {
                var correctAnswerLower = correctAnswer.ToLower();
                if (givenAnswerlower == correctAnswerLower)
                {
                    Debug.Log($"{givenAnswer} is Correct Answer!!");
                    _givenCorrectAnswer = true;
                }
                else
                {
                    if(_givenCorrectAnswer) return;
                        _givenCorrectAnswer = false;
                }
            }

            if (!_givenCorrectAnswer)
            {
                Debug.Log($"{givenAnswer} is InCorrect Answer!!");
            }
        }
        
    }
}
