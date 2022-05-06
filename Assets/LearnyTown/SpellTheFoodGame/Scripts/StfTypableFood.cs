using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LearnyTown.SpellTheFoodGame
{
    public class StfTypableFood : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _letterText;
        [SerializeField] private MeshRenderer _foodRenderer;

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
            _letterText.SetText(_defaultKey);
        }
        
        public void SetCorrectAnswer(Color greenColor)
        {
            _letterText.color = greenColor;
            // _foodRenderer.enabled = false;
            // Debug.Log($"Correct Answer: {_defaultKey}");
        }
        
        public void SetWrongAnswer(Color redColor, char wrongKey)
        {
            var tempKey = wrongKey.ToString().ToUpper();
            _letterText.SetText(tempKey);
            _letterText.color = redColor;
            // Debug.Log($"wrong Answer: {wrongKey}");

        }
        
        public void ResetKey()
        {
            _letterText.color = _defaultColor;
            // _letterText.color = Color.white;
            _letterText.SetText(_defaultKey);
            // _foodRenderer.enabled = true;
            Debug.Log($"Reset!");

        }
    }
}
