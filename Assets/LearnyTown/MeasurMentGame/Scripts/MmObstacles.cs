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
        [SerializeField] private float _speed;
        [SerializeField] private bool _randomGaps;
        [Space] [SerializeField] private MmAsteroids _asteroids;

        public static event Action<int, float> OnEnteringLanePoint;


        private float _avgLaneXPos;
        private int _actualGap;
        private bool _enteredLanePoint;
        private bool _canMove;
        
        // Start is called before the first frame update
        void Start()
        {
            _canMove = true;
            if (_randomGaps)
            {
                int gap = Random.Range(1, 10);
                int offset = Random.Range(0, 10);
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
                OnEnteringLanePoint?.Invoke(_actualGap, _avgLaneXPos);
                _enteredLanePoint = true;
            }
        }
        
        private void MoveTowardsEnd(Vector3 target, float speed)
        {
            // transform.DOMove(target, _speed, false);
            var dir = target - transform.position;
            dir.Normalize();
            if (_canMove)
            {
                transform.position += dir * (speed * Time.fixedDeltaTime);
            }

            if (transform.position.z <= target.z)
            {
                Destroy(gameObject);
            }
        }

        public void SetAsteroids(int gap, int offSet)
        {
            var length = _asteroids.SpwanPoints.Count;
            var startingPoint = offSet;
            int actualGap;
            if ((gap + offSet) < length)
            {
                actualGap = gap;
            }
            else
            {
                actualGap = Mathf.Clamp(gap, 0, length - offSet);
            }

            // Debug.Log($"starting Point: {startingPoint}, actualGap: {actualGap}");
            for (int i = startingPoint; i < startingPoint + actualGap; i++)
            {
                var obj = _asteroids.SpwanPoints[i].transform.GetChild(0);
                // Debug.Log($"{_asteroids.SpwanPoints[i].name}  child: {obj.name}");
                _avgLaneXPos += obj.position.x;
                obj.gameObject.SetActive(false);
            }

            _actualGap = actualGap;
            _avgLaneXPos /= actualGap;
        }
    }

    [Serializable]
    public class MmAsteroids
    {
        public int OffSet;
        public int Gap;
        public List<Transform> SpwanPoints;
        public GameObject AsteroidPrefab;
    }
}
