using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LearnyTown.ConstructionGame
{
    public class McqGame : MonoBehaviour
    {
        [SerializeField] private string _correctAnswer;
        [SerializeField] private List<AnswerButton> _answerButtons;

        // Start is called before the first frame update
        void Start()
        {
            SetUpButtons();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void SetUpButtons()
        {
            foreach (var answerButton in _answerButtons)
            {
                answerButton.Button.transform.GetChild(0).TryGetComponent<TextMeshProUGUI>(out var tmProUGUI);

                if (tmProUGUI != null)
                {
                    tmProUGUI.text = answerButton.Choice;
                }
                
                answerButton.Button.onClick.AddListener(delegate { CheckAnswer(answerButton.Choice); });
            }
        }
        private void CheckAnswer(string choice)
        {
            if (choice == _correctAnswer)
            {
                Debug.Log($"{choice} is Correct answer!!");
            }
            else
            {
                Debug.Log($"{choice} is InCorrect Answer!!");
            }
        }
    }

    [Serializable]
    public class AnswerButton
    {
        public string Choice;
        public Button Button;
    }
}
