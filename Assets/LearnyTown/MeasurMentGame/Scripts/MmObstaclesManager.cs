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

        [Space] [SerializeField] private Transform _spawnTransform;
        [SerializeField] private Transform _destoryTransform;
        [SerializeField] private GameObject ObstacleLinePrefab;


        public static event Action<int, float> ObstacleRegistered; 
        // public static float LaneEntryPointX;
        public static float LaneEntryPointZ;


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
        }

        private void OnDisable()
        {
            MmObstacles.OnEnteringLanePoint -= SetLaneEntryPoint;
        }

        // Update is called once per frame
        void Update()
        {
            SpawnObstacleLine();
        }

        private void SetLaneEntryPoint(int gap, float lanePosX)
        {
            _laneEntryPoint.position = new Vector3(lanePosX, _laneEntryPoint.position.y, _laneEntryPoint.position.z);
            Debug.Log($"Gap: {gap}");
            ObstacleRegistered?.Invoke(gap, lanePosX);
        }

        private void SpawnObstacleLine()
        {
            if (_nextSpawnTime >= _spwanTime)
            {
                var obj = Instantiate(ObstacleLinePrefab, _spawnTransform.position, Quaternion.identity);
                obj.TryGetComponent<MmObstacles>(out var mmObstacles);
                mmObstacles._targetEndPoint = _destoryTransform;
                _nextSpawnTime = 0;
            }
            else
            {
                _nextSpawnTime += Time.deltaTime;
            }
        }
    }
}
