using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LearnyTown.SpellTheFoodGame
{
    public class StfTypableFoodManager : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _deSpawnPoint;
        [SerializeField] private float _slidingDuration;
        
        [Space]
        [SerializeField] private StfJuicerController _juicerController;
        [SerializeField] private List<StfTypableFoodCollection> _typableFoods;

        private int _currentIndex;
        private int _nextIndex;
        
        public static event Action<char> OnInputChar; 

        
        private void OnEnable()
        {
            Keyboard.current.onTextInput += CheckForInput;
            StfTypableFoodCollection.OnCorrectInput += UpdateJuicer;
        }

        private void OnDisable()
        {
            Keyboard.current.onTextInput -= CheckForInput;
            StfTypableFoodCollection.OnCorrectInput -= UpdateJuicer;
        }

        // Start is called before the first frame update
        void Start()
        {
            _currentIndex = 0;
            _nextIndex = 0;
            
            foreach (var typableFood in _typableFoods)
            {
                typableFood.gameObject.SetActive(false);
                typableFood.SetInputTakingStatus(false);
                
            }
            SetFood(_nextIndex);
        }
        // Update is called once per frame
        void Update()
        {
        
        }

        private void SetFood(int foodIndex)
        {
            if (_typableFoods[foodIndex] != null)
            {
                _typableFoods[foodIndex].gameObject.SetActive(true);
                _typableFoods[foodIndex].transform.DOMove(transform.position, _slidingDuration)
                    .SetEase(Ease.InElastic)
                    .OnComplete(() => _typableFoods[foodIndex].SetInputTakingStatus(true));
                _currentIndex = foodIndex;
                _nextIndex = _currentIndex++;
            }
        }

        private void CheckForNextFood()
        {
            Debug.Log("YouWin!");
        }

        private void CheckForInput(char currentKey)
        {
            // Debug.Log($"Current key : {currentKey}");
            OnInputChar?.Invoke(currentKey);
        }

        private void UpdateJuicer(StfJuicerData stfJuicerData)
        {
            _juicerController.UpdateJuicer(stfJuicerData);
        }
    }
}
