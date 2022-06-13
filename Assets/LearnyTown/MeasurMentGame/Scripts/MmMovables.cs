using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnyTown.MeasurMentGame
{
    public class MmMovables : MonoBehaviour
    {
        [SerializeField] private bool _destroyAfterEndPoint;
        [SerializeField] private float _targetRespawnDistance;
        [SerializeField] private float _targetEndDistance;
        [SerializeField] private float _speed;
        
        private bool _canMove;
        
        // Start is called before the first frame update
        void Start()
        {
            _canMove = true;
        }

        // Update is called once per frame
        void Update()
        {
            MoveTowardsEnd(_targetEndDistance, _speed);
        }
        
        private void MoveTowardsEnd(float target, float speed)
        {
            // transform.DOMove(target, _speed, false);
            
            // var dir = target - transform.position;
            // dir.Normalize();

            if (transform.position.z <= target)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y,_targetRespawnDistance);
                if (_destroyAfterEndPoint)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                transform.position += new Vector3(0, 0, -1)  * speed * Time.fixedDeltaTime;
            }
        }
    }
}
