using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebGLSnailGame;
using Random = UnityEngine.Random;

namespace WebGLSnailGame
{
    public class DirectionKeysManager : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _spawnTimeGap;
        [SerializeField] private float _spwanMinGap;
        [SerializeField] private float _spwanMaxGap;
        [SerializeField] private Vector2 _startingPos;
        [SerializeField] private Vector2 _movementDirection;

        // [Space] [SerializeField] private int _startingNoOfKeys = 4;
        [SerializeField] private List<GameObject> _keyPrefabs;

        private RectTransform _rectTransform;
        private Vector2 _spawningPos;

        private float _cachedTime = 0;
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        // Start is called before the first frame update
        void Start()
        {
            DirectionKeyUI.Speed = _speed;
            DirectionKeyUI.Direction = _movementDirection;
            _spawningPos = _startingPos;

        }

        // Update is called once per frame
        void Update()
        {
            SpawnKeys(_spawnTimeGap);
        }

        private void OnEnable()
        {
            // DirectionDetector.OnCorrectKey += SpawnKeys;
            // DirectionKeyUI.OnDestroyKey += SpawnKeys;
        }
        private void OnDisable()
        {
            // DirectionDetector.OnCorrectKey -= SpawnKeys;
            // DirectionKeyUI.OnDestroyKey -= SpawnKeys;

        }

        private void SpawnKeys(float spawnTimeGap)
        {
            if (_cachedTime >= spawnTimeGap)
            {
                var randomGap = Random.Range(_spwanMinGap, _spwanMaxGap);
                var randomKey = _keyPrefabs[Random.Range(0, _keyPrefabs.Count)];
                var spawnedKey = Instantiate(randomKey, _rectTransform);
                spawnedKey.SetActive(false);
            
                spawnedKey.GetComponent<RectTransform>().anchoredPosition = new Vector2(_spawningPos.x + randomGap, _spawningPos.y);
                spawnedKey.SetActive(true);
                _cachedTime = 0;
            }
            else
            {
                _cachedTime += Time.deltaTime;
            }
            
        }
    }
}
