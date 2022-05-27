using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace LearnyTown.MathRacingGame
{
    public class MrUiManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _problemText;
        [SerializeField] private TMP_InputField _answerText;
        [SerializeField] private List<Button> _answerMcqButtons;
        [SerializeField] private TextMeshProUGUI _scoreText;

        public static event Action<int> OnAnswer;
        
        private enum MrQsMode
        {
            Addition, Subtraction, Multiplication, Division
        }
        private struct MrQuestionDetails
        {
            internal MrQsMode QsMode;
            internal int Num1;
            internal int Num2;
            internal float Ans;
            internal string Sign;
        }

        private MrQuestionDetails _currentQuestion;
        private float _currentAnswer;
        private string _currentAnswerText;
        private int _score;

        private List<MrMcqButton> _mcqButtons;
        private int _noOfMcqButtons;
        
        [Serializable]
        internal class MrMcqButton
        {
            internal Button McqButton;
            internal TextMeshProUGUI McqText;
            
            public MrMcqButton(Button button, TextMeshProUGUI mcqText)
            {
                McqButton = button;
                McqText = mcqText;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            _noOfMcqButtons = _answerMcqButtons.Count;
            SetMqcButtons();
            
            SetQuestions();
        }

        // Update is called once per frame
        void Update()
        {
            // if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            // {
            //     OnAnswer?.Invoke(1);
            // }
            // if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            // {
            //     OnAnswer?.Invoke(-1);
            // }

            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                CheckAnswer();
            }
        }

        [ContextMenu("Set Question")]
        private void SetQuestions()
        {
            ChooseQuestion();

            _currentAnswer = _currentQuestion.Ans;
            
            string question = $"{_currentQuestion.Num1} {_currentQuestion.Sign} {_currentQuestion.Num2}";
            
            _problemText.SetText(question);
            
            SetMcqAnswers();
        }

        private void CheckAnswer()
        {
            if (_answerText.text.Equals(_currentAnswer.ToString(CultureInfo.InvariantCulture)))
            {
                _answerText.text = String.Empty;
                OnAnswer?.Invoke(-1);
                
                _score += 100;
                
            }
            else
            {
                _answerText.text = String.Empty;
                OnAnswer?.Invoke(1);
                
                _score -= 100;
                
            }
            
            SetQuestions();

            _score = Mathf.Clamp(_score, 0, _score);
            _scoreText.SetText($"Score: {_score}");
        }

        private void ChooseQuestion()
        {
            float ans = 0;

            var num1 = Random.Range(0, 11);
            var num2 = Random.Range(0, 11);
            var mode = Random.Range(0, 4);

            switch (mode)
            {
                case 0: // sum
                    _currentQuestion.QsMode = MrQsMode.Addition;
                    _currentQuestion.Sign = "+";
                    ans = num1 + num2;
                    break;
                
                case 1: // multiplication
                    _currentQuestion.QsMode = MrQsMode.Multiplication;
                    _currentQuestion.Sign = "*";
                    ans = num1 * num2;
                    break;
                
                case 2: // subtraction
                    if (num1 < num2)
                    {
                        (num2, num1) = (num1, num2);
                    }
                    _currentQuestion.QsMode = MrQsMode.Subtraction;
                    _currentQuestion.Sign = "-";
                    ans = num1 - num2;
                    break;
                
                // case 3: // divide
                //     _question.QsMode = MrQsMode.Division;
                //     ans = num1 * num2;
                //     _question.Sign = "+";
                //     break;
                default:
                    _currentQuestion.QsMode = MrQsMode.Addition;
                    _currentQuestion.Sign = "+";
                    ans = num1 + num2;
                    break;
            }

            _currentQuestion.Num1 = num1;
            _currentQuestion.Num2 = num2;
            _currentQuestion.Ans = ans;
        }
        
        private void SetMqcButtons()
        {
            if (_answerMcqButtons == null) return;

            _mcqButtons = new List<MrMcqButton>(_noOfMcqButtons);

            int i = 0;
            foreach (var mcqButton in _answerMcqButtons)
            {
                var button = mcqButton;
                var text = mcqButton.GetComponentInChildren<TextMeshProUGUI>();
                
                _mcqButtons.Add(new MrMcqButton(button, text));
                
                var i1 = i;
                _mcqButtons[i].McqButton.onClick.AddListener(() => CheckMcqAnswer(_mcqButtons[i1].McqText.text));

                i++;
            }
        }

        private void SetMcqAnswers()
        {
            int GetRandomAns(int upperLimit) { return Random.Range(0, upperLimit); }

            int correctAnsIndex = GetRandomAns(_noOfMcqButtons);
            
            for (int i = 0; i < _noOfMcqButtons; i++)
            {
                if (i == correctAnsIndex)
                {
                    _mcqButtons[i].McqText.text = _currentAnswer.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    _mcqButtons[i].McqText.text = GetRandomAns(100).ToString(CultureInfo.InvariantCulture);
                }
            }
        }
        private void CheckMcqAnswer(string mcqAns)
        {
            // Debug.Log($"{mcqAns}");
            if (mcqAns.Equals(_currentAnswer.ToString(CultureInfo.InvariantCulture)))
            {
                _answerText.text = String.Empty;
                OnAnswer?.Invoke(-1);
                
                _score += 100;
                
            }
            else
            {
                _answerText.text = String.Empty;
                OnAnswer?.Invoke(1);
                
                _score -= 100;
                
            }
            
            SetQuestions();

            _score = Mathf.Clamp(_score, 0, _score);
            _scoreText.SetText($"Score: {_score}");
        }
    }
}
