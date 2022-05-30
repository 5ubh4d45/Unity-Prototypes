using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace LearnyTown.NumberMatchingGame
{
    public class NmPuzzleManager : MonoBehaviour
    {

        [SerializeField] private Vector2Int _gridSize;
        [SerializeField] private float _cellSize;
        
        [Space]
        [SerializeField] private List<NmPuzzlePiece> _puzzlePieces;


        private int _totalTypesOfPieces;
        private int _totalNoOfPieces;
        private float _totalWidth;
        private float _totalHeight;
        private Vector3 _upperLeftStartingPoint;
        private Vector3 _spawningPoint;

        [HideInInspector][SerializeField] private List<NmPuzzlePiece> _spawnedPuzzlePieces;

        private PlayerActions _playerActions; 

        private void OnEnable()
        {
            _playerActions = new PlayerActions();
            _playerActions.Enable();
            _playerActions.PlayerActionMap.PrimaryContact.started += OnClick;
        }

        private void OnDisable()
        {
            _playerActions.Disable();
            _playerActions.PlayerActionMap.PrimaryContact.started -= OnClick;

        }

        // Start is called before the first frame update
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {
           
        }

        private void OnClick(InputAction.CallbackContext ctx)
        {
            var pos = _playerActions.PlayerActionMap.PrimaryPosition.ReadValue<Vector2>();
            DoPuzzleCheck(pos);
            // Debug.Log($"Clicked! {pos}");
        }

        private void DoPuzzleCheck(Vector2 mousePoint)
        {
            var ray = Camera.main.ScreenPointToRay(mousePoint);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);

            Physics.Raycast(ray, out RaycastHit hit, 30f);
            // Debug.Log($"{hit.collider.name}");
            
            var obj = hit.collider;
            obj.TryGetComponent<NmPuzzlePiece>(out var puzzlePiece);

            if (puzzlePiece != null)
            {
                if (puzzlePiece.isOpen)
                {
                    puzzlePiece.ClosePuzzle();
                }
                else
                {
                    puzzlePiece.OpenPuzzle();
                }
            }
            
        }

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

            _spawnedPuzzlePieces = null;
        }

        [ContextMenu("Generate Grid")]
        private void GenerateGrid()
        {
            _totalTypesOfPieces = _puzzlePieces.Count;
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
                    int pieceIndex = Random.Range(0, _totalTypesOfPieces);
                    var puzzleObj = _puzzlePieces[pieceIndex];
                    
                    var spawnedObj = Instantiate(puzzleObj, transform);
                    spawnedObj.transform.localPosition = _spawningPoint;
                    
                    _spawnedPuzzlePieces.Add(spawnedObj.GetComponent<NmPuzzlePiece>());
                    
                    _spawningPoint.x += _cellSize;
                }

                _spawningPoint.z -= _cellSize;

            }

        }
    }
}
