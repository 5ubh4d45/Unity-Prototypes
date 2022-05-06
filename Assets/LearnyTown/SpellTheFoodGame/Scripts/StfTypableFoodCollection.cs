using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LearnyTown.SpellTheFoodGame
{
    public class StfTypableFoodCollection : MonoBehaviour
    {
    
        [SerializeField] private string _foodName;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _correctAnswerColor;
        [SerializeField] private Color _wrongAnswerColor;

        [Space] [SerializeField] private StfJuicerData _juicerData;
        [SerializeField] private float _juicerUnitLevel;
        
        [Space] [SerializeField] private Vector2 _spacing;
        [SerializeField] private float _offSetX;
        [SerializeField] private List<GameObject> _foodPrefabs;

        [Space] [SerializeField] private bool _hasGeneratedFoods;
        [SerializeField] private List<StfFoodObj> _spawnedFoods;
        
        
        private char _inputKey;
        private int _currentFoodLetterIndex = 0;
        private int _noOfCorrectAnswer = 0;
        private bool _completedWord;
        private bool _gotAnswer;


        public static event Action<StfJuicerData> OnCorrectAnswer;
        public static event Action<StfJuicerData> OnResetAnswer; 
        public static event Action<StfJuicerData> OnCorreectInput;


        private void OnEnable()
        {
            StfTypableFoodManager.OnInputChar += CheckForInput;
        }

        private void OnDisable()
        {
            StfTypableFoodManager.OnInputChar -= CheckForInput;
        }


        // Start is called before the first frame update
        void Start()
        {
            // Debug.Log("startingTest");
            if (!_hasGeneratedFoods) GenerateFood();
            _juicerUnitLevel = 1f / (float)_foodName.Length;
        }

        // // Update is called once per frame
        // void Update()
        // {
        //
        // }

        [ContextMenu("Generate Food")]
        public void GenerateFood()
        {
            
            int minRange = 0;
            int maxRange = _foodPrefabs.Count; // as in int Random.Range is exclusive

            

            // clearing previous generation if not null
            if (_spawnedFoods != null)
            {
                foreach (var food in _spawnedFoods)
                {
                    if (food.typableFood == null) continue;
                    
                    DestroyImmediate(food.typableFood.gameObject);
                }
                _spawnedFoods = null;
            }
            
            _spawnedFoods = new List<StfFoodObj>(_foodName.Length);
            
            _hasGeneratedFoods = true;
            
            foreach (var letter in _foodName)
            {
                
                int randFoodIndex = Random.Range(minRange, maxRange);
                var foodToSpawn = _foodPrefabs[randFoodIndex];
                
                var spawnedObj =Instantiate(foodToSpawn, transform);
                var tempFoodObj = new StfFoodObj();
                
                tempFoodObj.letter = letter;
                tempFoodObj.typableFood = spawnedObj.GetComponent<StfTypableFood>();
                
                tempFoodObj.typableFood.SetDefaults(_defaultColor, letter);
                
                _spawnedFoods.Add(tempFoodObj);
                
                // foodToSpawn.SetDefaults(_defaultColor, letter);
                
                // Debug.Log($"temp letter {tempFoodObj.Letter}; current Letter {letter}");
            }

            // position foods
            PositionFood();
        }

        // [ContextMenu("Position Food")]
        private void PositionFood()
        {
            int totalNoOfFoods = _spawnedFoods.Count;
            int midFoodPoint = totalNoOfFoods / 2;

            float totalHalfSpaceX = totalNoOfFoods * _spacing.x * 0.5f;  // getting the mid space to offset
            // float totalHalfSpaceY = totalNoOfFoods * _space.y * 0.5f;

            float startingPointX = -totalHalfSpaceX; // Offsetting to left side
            float startingPointY = _spacing.y;

            float actualStartingPositionX = startingPointX + _offSetX;
            
            for (int i = 0; i < totalNoOfFoods; i++)
            {
                var spawnedFood = _spawnedFoods[i].typableFood;
                
                Vector3 spawningPos = new Vector3(actualStartingPositionX, startingPointY, transform.position.z);
                actualStartingPositionX += _spacing.x;

                spawnedFood.transform.localPosition = spawningPos;
                
                // Debug.Log($"Positioning {spawningPos}, index {i}, letter {_spawnedFoods[i].letter}, name {_spawnedFoods[i].typableFood.name}");
            }
        }

        private void CheckForInput(char currentKey)
        {
            _inputKey = currentKey;
            if (_gotAnswer) return;

            if (_completedWord)
            {
                _completedWord = false;
                _currentFoodLetterIndex = 0;
                _noOfCorrectAnswer = 0;
                _juicerData.JuicerLevel = 0;
                
                OnResetAnswer?.Invoke(_juicerData);
                foreach (var food in _spawnedFoods)
                {
                    food.typableFood.ResetKey();
                }
            }

            if (_foodName[_currentFoodLetterIndex].Equals(_inputKey))
            {
                _spawnedFoods[_currentFoodLetterIndex].typableFood.SetCorrectAnswer(_correctAnswerColor);
                _noOfCorrectAnswer++;

                _juicerData.JuicerLevel += _juicerUnitLevel;
                OnCorreectInput?.Invoke(_juicerData);
            }
            else if (!_foodName[_currentFoodLetterIndex].Equals(_inputKey))
            {
                _spawnedFoods[_currentFoodLetterIndex].typableFood.SetWrongAnswer(_wrongAnswerColor, _inputKey);
                _noOfCorrectAnswer--;
            }

            _currentFoodLetterIndex++;

            if (_noOfCorrectAnswer == _foodName.Length) _gotAnswer = true;
            
            if (_currentFoodLetterIndex >= _foodName.Length) _completedWord = true;



        }
    }

    [Serializable]
    public class StfFoodObj
    {
        public char letter;
        public StfTypableFood typableFood;
    }
}
