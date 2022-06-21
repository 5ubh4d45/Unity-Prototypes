using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnyTown.FractionFootballGame
{
    public class FfBallController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Vector3 _startingPos;


        
        // Start is called before the first frame update
        void Start()
        {
            // _startingPos = transform.position;
        }
        
        internal void KickBall(Vector3 force)
        {
            _rb.AddForce(force, ForceMode.Impulse);
        }

        internal void Reset()
        {
            transform.position = _startingPos;
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }
    }
}
