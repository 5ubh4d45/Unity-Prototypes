using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CraftGame;

namespace CraftGame
{

    public class PaperColorManager : MonoBehaviour
    {
        [SerializeField] private List<PaperColorButtons> _paperColorButtons;

        public static event Action<Color, Color> OnSetPaperColor;

        // Start is called before the first frame update
        void Start()
        {
            foreach (var paperColorButton in _paperColorButtons)
            {
                paperColorButton.Button.image.color = paperColorButton.ButtonColor;
                paperColorButton.Button.onClick.AddListener(delegate
                {
                    SetPaperColor(paperColorButton.PaperColor, paperColorButton.ButtonColor);
                });
            }

            SetPaperColor(_paperColorButtons[1].PaperColor, _paperColorButtons[1].ButtonColor);
        }

        private void OnDestroy()
        {
            foreach (var paperColorButton in _paperColorButtons)
            {
                paperColorButton.Button.onClick.RemoveListener(delegate
                {
                    SetPaperColor(paperColorButton.PaperColor, paperColorButton.ButtonColor);
                });
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetPaperColor(Color paperColor, Color partColor)
        {
            OnSetPaperColor?.Invoke(paperColor, partColor);
            // Debug.Log($"{color}");
        }

        public void SetPartColor(Color color)
        {

        }
    }

    [System.Serializable]
    public struct PaperColorButtons
    {
        public Button Button;
        public Color ButtonColor;
        public Color PaperColor;
    }
}
