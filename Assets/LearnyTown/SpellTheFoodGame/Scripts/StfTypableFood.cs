using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LearnyTown.SpellTheFoodGame
{
    public class StfTypableFood : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _letterText;


        private string _defaultKey;
        private Color _defaultColor;
        
        // Start is called before the first frame update
        void Start()
        {
            _defaultKey = _letterText.text;
            _defaultColor = _letterText.color;
        }

        // // Update is called once per frame
        // void Update()
        // {
        //
        // }

        public void SetDefaults(Color defaultColor, char defaultChar)
        {
            _defaultColor = defaultColor;
            _defaultKey = defaultChar.ToString().ToUpper();
            _letterText.text = _defaultKey;
        }
        
        public void SetCorrectAnswer(Color greenColor)
        {
            _letterText.color = greenColor;
        }
        
        public void SetWrongAnswer(Color redColor, char wrongKey)
        {
            _letterText.color = redColor;
            _letterText.text = wrongKey.ToString().ToUpper();
        }
        
        public void ResetColor()
        {
            _letterText.color = _defaultColor;
            // _letterText.color = Color.white;
            _letterText.text = _defaultKey;
        }
    }
}
