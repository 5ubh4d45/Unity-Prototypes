using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LearnyTown.MeasurMentGame
{
    public class MmButtonPanel : MonoBehaviour
    {
        [SerializeField] private List<MmUIButtons> _uiButtons;


        public static event Action<int> OnButtonPressed; 

        // Start is called before the first frame update
        void Start()
        {

            foreach (var uiButton in _uiButtons)
            {
                uiButton.Button.onClick.AddListener(delegate { ButtonPressed(uiButton.Number); });
            }
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void ButtonPressed(int number)
        {
            OnButtonPressed?.Invoke(number);
        }
        
    }

    [Serializable]
    public class MmUIButtons
    { 
        public int Number;
        public Button Button;
    }
}
