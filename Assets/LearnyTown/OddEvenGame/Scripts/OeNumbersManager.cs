using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;


namespace LearnyTown.OddEvenGame
{
    public class OeNumbersManager : MonoBehaviour
    {
        [SerializeField] private float _radius;
        public float Radius => _radius;

        [SerializeField] private int _noOfNumbers;
        [SerializeField] private float _rotationOffSet;
        [SerializeField] private Transform _spawnerTransform;

        [SerializeField] private GameObject _numberPrefab;

        [Space] [SerializeField] private float _rotationAmount;
        [SerializeField] private float _rotationTime;
        [SerializeField] private float _rotationAmplitude;
        [SerializeField] private float _rotationPeriod;

        [Space] [SerializeField] private float _spawnInterval;
        [SerializeField] private float _numberSpeed;
        private float _nextSpawnTime;

        [SerializeField] private List<GameObject> _spawnedPrefabs;
        [SerializeField] private Vector3[] _posLists;
        private float _incrementAngleInRad;
        private float _offSetInRad;

        private bool _canRotate = true;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                RotateTunnel(OeMoveType.Left);
                // Debug.Log($"Leftkey Pressed");
            }
            if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                RotateTunnel(OeMoveType.Right);
                // Debug.Log($"Rightkey Pressed");
            }
            SpawnNumbers(_spawnInterval, false);
        }

        [ContextMenu("Generate Numbers")]
        private void GenerateNumbers()
        {
            // float incrementAngleRad = 360f * Mathf.Deg2Rad / _noOfNumbers;
            _incrementAngleInRad = 2f * Mathf.PI / _noOfNumbers;
            _offSetInRad = Mathf.Deg2Rad * _rotationOffSet;
            float r = _radius;
            float posX = 0f, posY = 0f, theta = 0f + _offSetInRad;
            _posLists = new Vector3[_noOfNumbers];


            for (int i = 0; i < _noOfNumbers; i++)
            {
                theta += _incrementAngleInRad;
                
                posX = r * Mathf.Cos(theta);
                posY = r * Mathf.Sin(theta);

                _posLists[i] = new Vector3(posX, posY, _spawnerTransform.position.z);

            }

            if (_spawnedPrefabs != null)
            {
                foreach (var prefab in _spawnedPrefabs)
                {
                        
#if UNITY_EDITOR
                    
                    DestroyImmediate(prefab);
                    
#endif

// #if UNITY_WEBGL
//                     
//                     Destroy(prefab);
// #endif

                }
            }

            _spawnedPrefabs = new List<GameObject>(_noOfNumbers);
            
            // foreach (var posList in _posLists)
            // {
            //     _spawnedPrefabs.Add(Instantiate(_numberPrefab, posList, Quaternion.identity, _spawnerTransform));
            //     // _spawnedPrefabs.Add(Instantiate(_numberPrefab, posList + _spawnerTransform.position, Quaternion.identity, _spawnerTransform));
            // }
            SpawnNumbers(0f, true);

        }

        private void RotateTunnel(OeMoveType moveType)
        {
            if (!_canRotate) return;
            
            switch (moveType)
            {
                case OeMoveType.Left :
                    _canRotate = false;
                    transform.DORotate(new Vector3(0, 0, _rotationAmount), _rotationTime, RotateMode.WorldAxisAdd)
                        .SetEase(Ease.OutElastic, _rotationAmplitude, _rotationPeriod).OnComplete(() => _canRotate = true);
                    break;
                    
                case OeMoveType.Right :                    
                    _canRotate = false;
                    transform.DORotate(new Vector3(0, 0, -_rotationAmount), _rotationTime, RotateMode.WorldAxisAdd)
                        .SetEase(Ease.OutElastic, _rotationAmplitude, _rotationPeriod).OnComplete(() => _canRotate = true);
                    break;
            }
            
            
        }

        private void SpawnNumbers(float spawnInterval, bool isEditorGenerated)
        {

            if (_nextSpawnTime >= spawnInterval)
            {
                foreach (var posList in _posLists)
                {
                    if (isEditorGenerated)
                    {
                        // _spawnedPrefabs.Add(Instantiate(_numberPrefab, posList + _spawnerTransform.position, Quaternion.identity, _spawnerTransform));
                        var obj = Instantiate(_numberPrefab, posList, Quaternion.identity, _spawnerTransform);
                        obj.TryGetComponent(out OeNumbers oeNumbers);
                        oeNumbers.SetStatus(_numberSpeed);
                        _spawnedPrefabs.Add(obj);
                        
                    }
                    else
                    {
                        var obj = Instantiate(_numberPrefab, posList, Quaternion.identity, _spawnerTransform);
                        obj.TryGetComponent(out OeNumbers oeNumbers);
                        oeNumbers.SetStatus(_numberSpeed);
                    }
                }

                _nextSpawnTime = 0f;
            }

            _nextSpawnTime += Time.deltaTime;
        }


    }

    public enum OeMoveType
    {
        Left, Right
    }
}
