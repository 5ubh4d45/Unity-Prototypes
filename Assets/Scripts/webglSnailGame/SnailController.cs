using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEngine;
using WebGLSnailGame;

namespace WebGLSnailGame
{
    public class SnailController : MonoBehaviour
    {
        [SerializeField] private PathFollower _pathFollower;
        [SerializeField] private MeshRenderer _snailRenderer;

        [SerializeField] private float _speedBoost;
        [SerializeField] private float _speedNerf;

        [SerializeField] private float _accelaration;
        [SerializeField] private float _deAccelaration;
        [SerializeField] private float _maxSpeed;

        private float _cachedSpeed;
        private Material _savedMat;
        private Color _savedColor;

        // Start is called before the first frame update
        void Start()
        {
            _savedMat = _snailRenderer.material;
            _savedColor = _savedMat.color;

        }

        private void OnEnable()
        {
            DirectionDetector.OnCorrectKey += BoostSpeed;
            DirectionDetector.OnIncorrectKey += NerfSpeed;
        }
        private void OnDisable()
        {
            DirectionDetector.OnCorrectKey -= BoostSpeed;
            DirectionDetector.OnIncorrectKey -= NerfSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            // if (_deAccelaration > 0) { _deAccelaration = 0;}
            // _deAccelaration = Mathf.Clamp(_deAccelaration, 0f, _maxDeAccelartion);

            _cachedSpeed += _accelaration * Time.deltaTime;

            // if (_cachedSpeed < 0) { _cachedSpeed = 0; }
            _cachedSpeed = Mathf.Clamp(_cachedSpeed, 0f, _maxSpeed);
            
            _pathFollower.speed = _cachedSpeed;
        }

        private void BoostSpeed(KeyDirection keyDirection)
        {
            // _deAccelaration -= _speedBoost;
            // _cachedSpeed -= _deAccelaration * Time.deltaTime;
            StartCoroutine(FlashSnail(Color.green, 0.2f));
        }

        private void NerfSpeed(KeyDirection keyDirection)
        {
            // _deAccelaration += _speedNerf;
            // _cachedSpeed -= _deAccelaration * Time.deltaTime;
            _cachedSpeed -= _speedNerf;
            StartCoroutine(FlashSnail(Color.red, 0.3f));
            
        }

        private IEnumerator FlashSnail(Color color, float interValTime)
        {
            _savedMat.color = color;
            yield return new WaitForSeconds(interValTime);
            
            _savedMat.color = _savedColor;
            yield return new WaitForSeconds(interValTime);
            
            _savedMat.color = color;
            yield return new WaitForSeconds(interValTime);
            
            _savedMat.color = _savedColor;
           
        }
    }
}
