using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace LearnyTown.MeasurMentGame
{
    public class MmScoreManager : MonoBehaviour
    {

        [SerializeField] private MMGamePlayerController _playerController;

        
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private List<Image> _hearts;


        public static event Action OnGameOver;
        public static MmScoreManager Instance;
        
        private int _numberOfHeartsDisabled = 0;
        private int _totalNumberOfHearts;
        private bool _gameOver;
        private int _score;
        public int Score => _score;

        // private void Awake()
        // {
        //     // if (Instance == null)
        //     // {
        //     //     Instance = this;
        //     // }
        //     // else
        //     // {
        //     //     Destroy(gameObject);
        //     // }
        // }

        // Start is called before the first frame update
        void Start()
        {
            _totalNumberOfHearts = _hearts.Count;

        }

        // Update is called once per frame
        void Update()
        {

            // test damage
            // if (Keyboard.current.spaceKey.wasPressedThisFrame)
            // {
            //     TakeDamage();
            // }
            CheckForGameOver();
        }

        public void SetScore(int scoreToAdd)
        {
            _score += scoreToAdd;
            if (_score < 0)
            {
                _score = 0;
            }
            _scoreText.text = $"Score: {_score}";
            if (scoreToAdd < 0)
            {
                TakeDamage();
            }
        }

        private void TakeDamage()
        {
            _numberOfHeartsDisabled++;

            if (_numberOfHeartsDisabled < 0 || _numberOfHeartsDisabled > _totalNumberOfHearts)
            {
                _numberOfHeartsDisabled = Mathf.Clamp(_numberOfHeartsDisabled, 0, _totalNumberOfHearts);
                
                return;
            }
            
            for (int i = 0; i < _numberOfHeartsDisabled; i++)
            {
                _hearts[i].gameObject.SetActive(false);
                _playerController.SetPlayerScale(1);
                _playerController.FlashEffect(Color.red, 4);
            }
        }

        private void CheckForGameOver()
        {
            if (_gameOver) return;

            if (_numberOfHeartsDisabled >= _totalNumberOfHearts)
            {
                _gameOver = true;
                OnGameOver?.Invoke();
            }
        }
    }
}
