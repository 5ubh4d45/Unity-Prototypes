using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

namespace LearnyTown.MeasurMentGame
{
    public class MmObstacles : MonoBehaviour
    {
        [SerializeField] public Transform _targetEndPoint;
        [SerializeField] public Transform _playerTransform;
        [SerializeField] private float _speed;
        [SerializeField] private bool _randomGaps;
        [Space] [SerializeField] private MmAsteroids _asteroids;

        public static event Action<int, float, GameObject> OnEnteringLanePoint;
        public static event Action<int> SetScore; 


        private float _avgLaneXPos;
        private int _actualGap;
        private bool _enteredLanePoint;
        private bool _canMove;
        public bool HasProperlyCrossedPlayer;
        public bool BehindPlayer;
        public bool GotCorrectAns;
        private bool _canSetScore = true;

        // Start is called before the first frame update
        void Start()
        {
            _canMove = true;
            if (_randomGaps)
            {
                var length = _asteroids.SpawnPoints.Count;
                var midPoint = length / 2;
                int offset = Random.Range(-midPoint, midPoint);
                int gap = Random.Range(1, length);
                SetAsteroids(gap, offset);
            }
            else
            {
                SetAsteroids(_asteroids.Gap, _asteroids.OffSet);
            }
            
        }

        // Update is called once per frame
        void Update()
        {
            MoveTowardsEnd(_targetEndPoint.position, _speed);

            if (transform.position.z <= MmObstaclesManager.LaneEntryPointZ && !_enteredLanePoint)
            {
                OnEnteringLanePoint?.Invoke(_actualGap, _avgLaneXPos, gameObject);
                _enteredLanePoint = true;
            }
            CheckForPlayer();
            CheckForScore();
        }
        
        private void MoveTowardsEnd(Vector3 target, float speed)
        {
            // transform.DOMove(target, _speed, false);
            var dir = target - transform.position;
            dir.Normalize();
            if (_canMove)
            {
                transform.position += dir * (speed * Time.fixedDeltaTime * Time.timeScale);
            }

            if (transform.position.z <= target.z)
            {
                Destroy(gameObject);
            }
        }

        private void CheckForPlayer()
        {
            // if(_playerTransform == null) return;
            if (HasProperlyCrossedPlayer) return;

            if (transform.position.z <= _playerTransform.position.z)
            {
                BehindPlayer = true;
            }

            if (BehindPlayer && GotCorrectAns)
            {
                HasProperlyCrossedPlayer = true;
            }
            // BehindPlayer = false;
        }

        public void SetAsteroids(int gap, int offSet)
        {
            var length = _asteroids.SpawnPoints.Count;
            var midPoint = length / 2;
            var startingPoint = offSet + midPoint;
            int actualGap;
            if ((gap + startingPoint) < length && (gap + startingPoint) > 0)
            {
                actualGap = gap;
            }
            else
            {
                actualGap = Mathf.Clamp(gap, 1, length - startingPoint);
            }
            

            foreach (var spawnPoint in _asteroids.SpawnPoints)
            {
                var scale = 1 + Random.Range(0, 0.3f);
                // var obj = spwanPoint.GetChild(0);
                spawnPoint.localScale = new Vector3(scale, scale, scale);
            }

            // Debug.Log($"starting Point: {startingPoint}, actualGap: {actualGap}");
            for (int i = startingPoint; i < startingPoint + actualGap; i++)
            {
                var obj = _asteroids.SpawnPoints[i].transform.GetChild(0);
                // Debug.Log($"{_asteroids.SpawnPoints[i].name}  child: {obj.name}");
                _avgLaneXPos += obj.position.x;
                obj.gameObject.SetActive(false);
            }

            _actualGap = actualGap;
            _avgLaneXPos /= actualGap;
        }
        
        private void CheckForScore()
        {
            // if( is null) return;
            
            if(!_canSetScore) return;
        
            if (HasProperlyCrossedPlayer)
            {
                // Debug.Log($"Cleared obstacle");
                SetScore?.Invoke(100);
                _canSetScore = false;
                return;
            }

            if (BehindPlayer && !HasProperlyCrossedPlayer)
            {
                // Debug.Log($"couldn't clear obstacles");
                // Debug.Log("Score: -100");
                SetScore?.Invoke(-100);

                _canSetScore = false;
                return;
            }
        }
        
    }

    [Serializable]
    public class MmAsteroids
    {
        public int OffSet;
        public int Gap;
        public List<Transform> SpawnPoints;
        // public GameObject AsteroidPrefab;
    }
}
