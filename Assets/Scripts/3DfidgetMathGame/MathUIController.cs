using System;
using System.Collections;
using System.Collections.Generic;
using FidgetMathGame;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FidgetMathGame
{


    public class MathUIController : MonoBehaviour
    {
        [Tooltip("Without the '='")]
        [SerializeField] private string _question;

        [SerializeField] private int _answer;
        [Space]
        [SerializeField] private TextMeshProUGUI _questionText;
        [SerializeField] private TextMeshProUGUI _answerText;
        
        [Space][SerializeField] private GameObject _startScreen;
        [SerializeField] private GameObject _finishScreen;
        [SerializeField] private int _nextSceneIndex;
        private void OnEnable()
        {
            FidgetBall.BallTapped += CheckAnswer;
        }

        private void OnDisable()
        {
            FidgetBall.BallTapped -= CheckAnswer;
        }
        
        void Awake()
        {
            BallController.CanTap = false;
        }

        // Start is called before the first frame update
        void Start()
        {
            _questionText.text = _question;
            _answerText.text = null;
            _finishScreen.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void CheckAnswer(int number)
        {
            if(number != _answer) return;
            _answerText.text = _answer.ToString();
            BallController.CanTap = false;
            _finishScreen.SetActive(true);
            Debug.Log("You Won!, Loading NextScene");
        }
        public void StartGame()
        {
            BallController.CanTap = true;
            _startScreen.SetActive(false);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void NextScene()
        {
            var currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
            // var nextIndex = currentBuildIndex++;
            SceneManager.LoadScene(_nextSceneIndex);
            // SceneManager.UnloadSceneAsync(currentBuildIndex);
        }
    }
}