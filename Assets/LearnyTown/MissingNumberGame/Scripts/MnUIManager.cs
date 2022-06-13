using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace LearnyTown.MissingNumberGame
{
    public class MnUIManager : MonoBehaviour
    {
        [SerializeField] private MnBridgeManager _bridgeManager;
        [SerializeField] private MnCarController _carController;
        [SerializeField] private float _inputWaitTime;


        [Space] [SerializeField] private List<Button> _buttons;
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _heartsText;
        [SerializeField] private GameObject _ansBox;
        [SerializeField] private GameObject _gameOverScr;

        internal float InputWaitTime => _inputWaitTime;

        internal static event Action<bool> OnAnswerResponse;

        [SerializeField] private List<TextMeshProUGUI> _buttonTexts;
        private List<int> _ansList;
        private int _correctAns;
        private int _score;
        private int _health;


        private bool _checkedAnswer;
        
        private void OnEnable()
        {
            MnBridgeManager.OnUpdatedAns += UpdateAnswer;
        }

        private void OnDestroy()
        {
            MnBridgeManager.OnUpdatedAns -= UpdateAnswer;
        }


        // Start is called before the first frame update
        void Start()
        {
            // foreach (var button in _buttons)
            // {
            //     var text = button.GetComponentInChildren<TextMeshProUGUI>();
            //     _buttonTexts.Add(text);
            // }
            for (int i = 0; i < _buttons.Count; i++)
            {
                var index = i;
                _buttons[index].onClick.AddListener((() => CheckAnswer(_buttonTexts[index].text)));
            }
            _ansBox.SetActive(false);
            _gameOverScr.SetActive(false);
            SetScore(000, 3);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void CheckAnswer(string pressedAns)
        {
            _checkedAnswer = true;
            if (pressedAns.Equals(_correctAns.ToString()))
            {
                OnAnswerResponse?.Invoke(true);
                
                SetScore(100, 0);

            }
            else
            {
                OnAnswerResponse?.Invoke(true);

                StartCoroutine(_carController.TakeDamage());
                SetScore(-100, -1);
            }

            if (_health <= 0)
            {
                OnAnswerResponse?.Invoke(false);
                _ansBox.SetActive(false);
                _scoreText.gameObject.SetActive(false);
                _heartsText.gameObject.SetActive(false);
                _gameOverScr.SetActive(true);
            }

            HideAnswerBox();
        }

        private void SetScore(int i, int i1)
        {
            _score += i;
            _score = Mathf.Clamp(_score, 0, _score);
            _health += i1;
            _health = Mathf.Clamp(_health, 0, _health);
            
            UpdateScore(_score, _health);
            
        }

        private void UpdateScore(int score, int hearts)
        {
            _scoreText.SetText($"SCORE: {score}");
            _heartsText.SetText($"HEART: {hearts}");
        }


        private void UpdateAnswer(List<int> ansList, int correctAns)
        {
            _ansList = ansList;
            _correctAns = correctAns;

            for (int i = 0; i < _buttons.Count; i++)
            {
                var rand = Random.Range(0, 100);
                _buttonTexts[i].SetText(rand.ToString());
            }

            int correctIndex = Random.Range(0, _buttons.Count);
            _buttonTexts[correctIndex].SetText(_correctAns.ToString());

            StartCoroutine(ShowAnswerBox());
        }

        private IEnumerator ShowAnswerBox()
        {
            _ansBox.SetActive(true);
            _slider.value = 0;
            float target = 0f;
            DOTween.To(() => target, x => target = x, 1f, _inputWaitTime)
                .OnUpdate((() => _slider.value = target))
                .OnComplete(delegate
                {
                    if (!_checkedAnswer)
                    {
                        // CheckAnswer("FALSE ANSWER");
                    }
                    HideAnswerBox();
                });
            
            yield return null;
        }

        private void HideAnswerBox()
        {
            _ansBox.SetActive(false);
            _slider.value = 0;
            _checkedAnswer = false;
        }
    }
}
