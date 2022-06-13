using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace LearnyTown.MeasurMentGame
{
    public class MmObstaclesManager : MonoBehaviour
    {

        [SerializeField] private Transform _laneEntryPoint;
        [SerializeField] private float _spwanTime;
        [SerializeField] private MmScoreManager _scoreManager;

        [Space] [SerializeField] private Transform _spawnTransform;
        [SerializeField] private Transform _destoryTransform;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private GameObject ObstacleLinePrefab;


        public static event Action<int, float, MmObstacles> ObstacleRegistered; 
        // public static float LaneEntryPointX;
        public static float LaneEntryPointZ;

        private MmObstacles _currentObstacle;
        private float _nextSpawnTime;

        // Start is called before the first frame update
        void Start()
        {
            // LaneEntryPointX = _laneEntryPoint.position.x;
            LaneEntryPointZ = _laneEntryPoint.position.z;
        }

        private void OnEnable()
        {
            MmObstacles.OnEnteringLanePoint += SetLaneEntryPoint;
            MmObstacles.SetScore += SetScore;
        }

        private void OnDisable()
        {
            MmObstacles.OnEnteringLanePoint -= SetLaneEntryPoint;
            MmObstacles.SetScore -= SetScore;
        }

        // Update is called once per frame
        void Update()
        {
            SpawnObstacleLine();
        }

        private void SetLaneEntryPoint(int gap, float lanePosX, GameObject obstacleOBJ)
        {
            _laneEntryPoint.position = new Vector3(lanePosX, _laneEntryPoint.position.y, _laneEntryPoint.position.z);
            // Debug.Log($"Gap: {gap}");
            _currentObstacle = obstacleOBJ.GetComponent<MmObstacles>();
            ObstacleRegistered?.Invoke(gap, lanePosX, _currentObstacle);
        }

        private void SpawnObstacleLine()
        {
            if (_nextSpawnTime >= _spwanTime)
            {
                var obj = Instantiate(ObstacleLinePrefab, _spawnTransform.position, Quaternion.identity, transform);
                obj.TryGetComponent<MmObstacles>(out var mmObstacles);
                mmObstacles._targetEndPoint = _destoryTransform;
                mmObstacles._playerTransform = _playerTransform;
                _nextSpawnTime = 0;
            }
            else
            {
                _nextSpawnTime += Time.deltaTime;
            }
        }

        private void SetScore(int scoreToAdd)
        {
            _scoreManager.SetScore(scoreToAdd);
        }
    }
}
