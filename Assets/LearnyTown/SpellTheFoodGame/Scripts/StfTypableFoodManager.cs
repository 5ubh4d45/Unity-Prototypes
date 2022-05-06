using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LearnyTown.SpellTheFoodGame
{
    public class StfTypableFoodManager : MonoBehaviour
    {

        [SerializeField] private StfJuicerController _juicerController;
        
        public static event Action<char> OnInputChar; 

        private void OnEnable()
        {
            Keyboard.current.onTextInput += CheckForInput;
            StfTypableFoodCollection.OnCorreectInput += UpdateJuicer;
        }

        private void OnDisable()
        {
            Keyboard.current.onTextInput -= CheckForInput;
            StfTypableFoodCollection.OnCorreectInput -= UpdateJuicer;
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }
        // Update is called once per frame
        void Update()
        {
        
        }

        private void CheckForInput(char currentKey)
        {
            Debug.Log($"Current key : {currentKey}");
            OnInputChar?.Invoke(currentKey);
        }

        private void UpdateJuicer(StfJuicerData stfJuicerData)
        {
            _juicerController.UpdateJuicer(stfJuicerData);
        }
    }
}
