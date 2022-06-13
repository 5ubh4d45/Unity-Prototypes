using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace LearnyTown.NumberMatchingGame
{
    public class NmPuzzleManager : MonoBehaviour
    {
        [Tooltip("Make Even Number Grid")]
        [SerializeField] private Vector2Int _gridSize;
        [SerializeField] private float _cellSize;
        [SerializeField] private float _openingTime;

        [Space] [SerializeField] private TextMeshProUGUI _scoreText;
        
        [Space]
        [SerializeField] private List<NmPuzzlePiece> _puzzlePieces;


        private int _noOfCorrectAnswers;
        private int _typesOfPieces;
        private int _totalNoOfPieces;
        private float _totalWidth;
        private float _totalHeight;
        private Vector3 _upperLeftStartingPoint;
        private Vector3 _spawningPoint;

        private NmPuzzlePiece _currentPuzzlePiece;
        private NmPuzzlePiece _previousPuzzlePiece;

        [SerializeField] private List<NmPuzzlePiece> _spawnedPuzzlePieces;

        private PlayerActions _playerActions;
        private bool _canClickPuzzle;
        private int _score;
        private Vector2Int _currentGrid;

        private void OnEnable()
        {
            _playerActions = new PlayerActions();
            _playerActions.Enable();
            _playerActions.PlayerActionMap.PrimaryContact.started += OnClick;

            NmUiManager.OnLevelSelection += StartLevel;
            NmUiManager.OnLevelRetry += () => StartLevel(_currentGrid);
            NmUiManager.OnLevelExit += EndLevel;
        }

        private void OnDisable()
        {
            _playerActions.Disable();
            _playerActions.PlayerActionMap.PrimaryContact.started -= OnClick;

        }

        // Start is called before the first frame update
        void Start()
        {
           // GenerateGrid();
           _canClickPuzzle = false;
           _currentGrid = _gridSize;
        }

        // Update is called once per frame
        void Update()
        {
           
        }

        #region LevelManagement

        private void EndLevel()
        {
            DOTween.Clear();
            _canClickPuzzle = false;
        }
        
        private void StartLevel(Vector2Int grid)
        {
            _currentGrid = grid;
            _gridSize = _currentGrid;
            _score = 0;
            _scoreText.SetText($"Score: {_score}");
            GenerateGrid();
        }

        #endregion

        #region Clicking And Interaction
        
        private void OnClick(InputAction.CallbackContext ctx)
        {
            if (!_canClickPuzzle) return;
            var pos = _playerActions.PlayerActionMap.PrimaryPosition.ReadValue<Vector2>();
            DoPuzzleCheck(pos);
            // Debug.Log($"Clicked! {pos}");
        }

        private void DoPuzzleCheck(Vector2 mousePoint)
        {
            var ray = Camera.main.ScreenPointToRay(mousePoint);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);

            Physics.Raycast(ray, out RaycastHit hit, 100f);
            // Debug.Log($"{hit.collider.name}");
            
            var obj = hit.collider;
            obj.TryGetComponent<NmPuzzlePiece>(out var puzzlePiece);
            
            if (puzzlePiece != null)
            {
                if(_previousPuzzlePiece != null) StartCoroutine(_previousPuzzlePiece.ClosePuzzle());
                
                _currentPuzzlePiece = puzzlePiece;
                if (_currentPuzzlePiece.isOpen)
                {
                    StartCoroutine(_currentPuzzlePiece.ClosePuzzle());
                }
                else
                {
                    StartCoroutine(_currentPuzzlePiece.OpenPuzzle());
                    // checking numbers
                    if (_previousPuzzlePiece != null)
                    {
                        if (_previousPuzzlePiece.GetNumber() == _currentPuzzlePiece.GetNumber())
                        {
                            StartCoroutine(_previousPuzzlePiece.OpenPuzzle());
                            
                            // score set
                            SetScore(100);
                            _noOfCorrectAnswers += 2;
                            
                            StartCoroutine(_previousPuzzlePiece.DeletePuzzle());
                            StartCoroutine(_currentPuzzlePiece.DeletePuzzle());
                        }
                        else
                        {
                            // score set
                            SetScore(-20);
                        }
                    }
                }
                
                _previousPuzzlePiece = _currentPuzzlePiece;
                
                if (_noOfCorrectAnswers >= _totalNoOfPieces)
                {
                    StartCoroutine(GenerateGridCoRoutine());
                }
            }
            
        }

        private void SetScore(int addedValue)
        {
            _score += addedValue;
            _score = Mathf.Clamp(_score, 0, _score); 
            _scoreText.SetText($"Score: {_score}");
        }
        
        #endregion

        #region GridControls
        // [ContextMenu("Clear Grid")]
        private void ClearGrid()
        {
            if (_spawnedPuzzlePieces == null) return;
            foreach (var spawnedPuzzle in _spawnedPuzzlePieces)
            {
                if (spawnedPuzzle != null)
                {
                    DestroyImmediate(spawnedPuzzle.gameObject);
                }
            }

            _noOfCorrectAnswers = 0;
            _spawnedPuzzlePieces = null;
        }

        internal IEnumerator GenerateGridCoRoutine()
        {
            yield return new WaitForSeconds(2f);
            GenerateGrid();
        }
        [ContextMenu("Generate Grid")]
        internal void GenerateGrid()
        {
            _typesOfPieces = _puzzlePieces.Count;
            _totalNoOfPieces = (_gridSize.x * _gridSize.y);
            
            
            _totalHeight = _cellSize * (_gridSize.x - 1f);
            _totalWidth = _cellSize * (_gridSize.y - 1f);
            
            // clears previous grids
            ClearGrid();

            _spawnedPuzzlePieces = new List<NmPuzzlePiece>(_totalNoOfPieces);
            
            // X and Z as the currentGame is on XZ Plane
            var upperLeftX = -(_totalWidth / 2);
            var upperLeftZ = (_totalHeight / 2);

            _upperLeftStartingPoint = new Vector3(upperLeftX, 0, upperLeftZ);

            // setting up spawning Point
            _spawningPoint = _upperLeftStartingPoint;
            for (int i = 0; i < _gridSize.x; i++)
            {
                // for each row
                _spawningPoint.x = _upperLeftStartingPoint.x;
                
                for (int j = 0; j < _gridSize.y; j++)
                {
                    // choosing random piece
                    int pieceIndex = Random.Range(0, _typesOfPieces);
                    var puzzleObj = _puzzlePieces[pieceIndex];
                    
                    var spawnedObj = Instantiate(puzzleObj, transform);
                    spawnedObj.transform.localPosition = _spawningPoint;
                    
                    _spawnedPuzzlePieces.Add(spawnedObj.GetComponent<NmPuzzlePiece>());
                    
                    _spawningPoint.x += _cellSize;
                }

                _spawningPoint.z -= _cellSize;

            }
            
            // // show numbers
            // for (int i = 0; i < _totalNoOfPieces; i++)
            // {
            //     Debug.Log($"Default Before: {_spawnedPuzzlePieces[i].gameObject.GetInstanceID()}");
            // }

            var tempPuzzles = _spawnedPuzzlePieces;
            // shuffling of the pieces
            for (int i = 0; i < tempPuzzles.Count; i++)
            {
                var temp = tempPuzzles[i];
                int randomIndex = Random.Range(0, tempPuzzles.Count);
                tempPuzzles[i] = tempPuzzles[randomIndex];
                tempPuzzles[randomIndex] = temp;
            }
            
            // setting values
            for (var i = 0; i < _totalNoOfPieces; i += 2)
            {
                var num = Random.Range(0, 100);
                tempPuzzles[i].SetNumber(num);
                tempPuzzles[i+1].SetNumber(num);
            }

            // for (int i = 0; i < _totalNoOfPieces; i++)
            // {
            //     Debug.Log($"Default {_spawnedPuzzlePieces[i].gameObject.GetInstanceID()}");
            //     Debug.Log($"Modified {tempPuzzles[i].gameObject.GetInstanceID()}");
            // }

            // show numbers
            StartCoroutine(PlayPuzzleStartingAnimation(_openingTime, _spawnedPuzzlePieces));
        }
        
        private IEnumerator PlayPuzzleStartingAnimation(float delay, List<NmPuzzlePiece> puzzlePieces)
        {
            _canClickPuzzle = false;
            foreach (var puzzle in puzzlePieces)
            {
                StartCoroutine(puzzle.OpenPuzzle());
                yield return new WaitForSeconds(delay);
                StartCoroutine(puzzle.ClosePuzzle());
                yield return new WaitForSeconds(delay);
            }

            _canClickPuzzle = true;
        }
        
        #endregion
    }
}
