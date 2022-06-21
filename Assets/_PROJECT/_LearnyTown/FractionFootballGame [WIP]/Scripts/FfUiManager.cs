using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;

namespace LearnyTown.FractionFootballGame
{
    public class FfUiManager : MonoBehaviour
    {

        [SerializeField] [TextArea(3, 5)] private string _fractionAns;
        [SerializeField] private TextMeshProUGUI _fractionText;

        [Space]
        [SerializeField] private GameObject _fractionBox;
        [SerializeField] private Slider _fractionSlider;
        [SerializeField] private float _inputWaitTime;
        [SerializeField] private int _numberOfLoops;
        [SerializeField] private Button _fractionButton;
        [SerializeField] private List<DecimalAnsOptions> _decimalOptions;

        [Space] 
        [SerializeField] private GameObject _shootBox;
        [SerializeField] private Button _shootButton;
        [SerializeField] private TextMeshProUGUI _shootBoxtext;
        [SerializeField] private Button _retyButton;

        // [Space] [SerializeField] 


        internal static event Action<bool> OnCorrectAnswerShoot;
        internal static event Action OnResetAnswer;

        private float _currentVal;
        private Tween _fractionTween;
        private bool _canCheckAnswer;
        private bool _isCorrectAns;

        // Start is called before the first frame update
        void Start()
        {
            _fractionButton.onClick.AddListener(CheckAnswer);
            foreach (var option in _decimalOptions)
            {
                option.text.SetText(option.ans);
            }
            _fractionText.SetText(_fractionAns);
            
            _shootButton.onClick.AddListener(() => StartCoroutine(SetKick()));
            _retyButton.onClick.AddListener(HideShootBox);
            HideShootBox();
        }

        [ContextMenu("Do Fraction Slider")]
        internal void DoFractionSlider()
        {
            float target = 0f;
            _canCheckAnswer = true;

            _fractionTween = DOTween.To(() => target, x => target = x, 1f, (float)(_inputWaitTime/ _numberOfLoops))
                .SetEase(Ease.Linear)
                .OnUpdate((delegate
                {
                    _fractionSlider.value = target;
                    _currentVal = target;
                }))
                .SetLoops(-1, LoopType.Yoyo)
                .OnComplete(delegate
                {
                    target = 0;
                    _currentVal = target;
                    _fractionSlider.value = 0;
                });
        }

        private void GenerateNextSetOfOption(string fractionString, float correctAns)
        {
            
        }

        private void CheckAnswer()
        {
            if (!_canCheckAnswer) return;

            foreach (var option in _decimalOptions)
            {
                if (option.IsInCorrectRange(_currentVal))
                {
                    if (option.isCorrectAns)
                    {
                        // correctAnswer
                        Debug.Log("CorrectAns");
                        _fractionTween.Kill();
                        _canCheckAnswer = false;
                        _isCorrectAns = true;
                        
                        ShowShootBox("Correct Answer!! \n Now Kick the Ball!!");
                        return;
                    }
                    else
                    {
                        Debug.Log("InCorrectAns");
                        _fractionTween.Kill();

                        _canCheckAnswer = false;

                        _isCorrectAns = false;
                        ShowShootBox("Incorrect Answer!! \n Now Kick the Ball!!");
                        return;
                    }
                }
            }
        }

        private void ShowShootBox(string shootBoxText)
        {
            _fractionBox.SetActive(false);
            _shootBoxtext.SetText(shootBoxText);
            _shootBox.SetActive(true);
        }

        private void HideShootBox()
        {
            _fractionBox.SetActive(true);
            _shootBox.SetActive(false);
            OnResetAnswer?.Invoke();
            DoFractionSlider();

        }
        
        private IEnumerator SetKick()
        {
            OnCorrectAnswerShoot?.Invoke(_isCorrectAns);
            yield return new WaitForSeconds(1f);
            
        }
        
    }

    [System.Serializable]
    public class DecimalAnsOptions
    {
        [SerializeField] internal TextMeshProUGUI text;
        [SerializeField] [TextArea(3, 5)] internal string ans;
        [SerializeField] internal bool isCorrectAns;

        [Tooltip("Value 0-1")] [Range(0f, 1f)] [SerializeField] internal float minLimit;
        [Tooltip("Value 0-1")] [Range(0f, 1f)] [SerializeField] internal float maxLimit;

        internal bool IsInCorrectRange(float correctAnswer)
        {
            return minLimit <= correctAnswer && correctAnswer <= maxLimit;
        }
        
    }
}
