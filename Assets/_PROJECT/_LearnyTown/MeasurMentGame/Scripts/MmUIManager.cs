using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LearnyTown.MeasurMentGame
{
    public class MmUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _startScreen;
        [SerializeField] private GameObject _gameOverScreen;
        [SerializeField] private TextMeshProUGUI _countDownText;

        
        // Start is called before the first frame update
        void Start()
        {
            _gameOverScreen.SetActive(false);

            Initiate();
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnEnable()
        {
            MmScoreManager.OnGameOver += ShowGameOverScreen;
        }

        private void OnDisable()
        {
            MmScoreManager.OnGameOver -= ShowGameOverScreen;
        }

        private void Initiate()
        {
            _startScreen.SetActive(true);
            _countDownText.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
        public void RestartGame()
        {
            // _startScreen.SetActive(true);
            // _countDownText.gameObject.SetActive(false);
            Time.timeScale = 1;
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }

        public void ProceedToGamePlay()
        {
            _startScreen.SetActive(false);
            _gameOverScreen.SetActive(false);
            Time.timeScale = 1;
            StartCoroutine(StartCountDown(0.5f));

        }

        public void ShowGameOverScreen()
        {
            Time.timeScale = 0;
            _gameOverScreen.SetActive(true);
        }

        private IEnumerator StartCountDown(float delayTime)
        {
            _countDownText.text = $"3";
            yield return new WaitForSeconds(delayTime);

            _countDownText.text = $"2";
            yield return new WaitForSeconds(delayTime);

            _countDownText.text = $"1";
            yield return new WaitForSeconds(delayTime);
            
            _countDownText.text = $"GO!!";
            yield return new WaitForSeconds(delayTime + 0.5f);

            _countDownText.gameObject.SetActive(false);
            yield return null;
        }
    }
}
