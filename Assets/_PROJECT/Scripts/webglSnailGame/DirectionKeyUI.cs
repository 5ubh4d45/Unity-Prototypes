using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebGLSnailGame;

namespace WebGLSnailGame
{
    [System.Serializable]
    public enum KeyDirection { Up, Down, Left, Right, None}
    public class DirectionKeyUI : MonoBehaviour
    {
        [SerializeField]private KeyDirection _keyDirection;
        
        private RectTransform _rectTransform;

        public static float Speed = 1f;
        public static Vector2 Direction;
        // public static float DetectorBoundLeft;
        public static float DetectorWidth;
        public static Vector2 DetectorPosition;

        public static event Action<KeyDirection, GameObject> OnEnterDetection;
        public static event Action OnExitingDetection;
        public static event Action OnDestroyKey;
        private static float _speedMultiplier = 20f;
        
        private bool _canPress = true;
        private bool _hasExited = false;

        private void Awake()
        {
            _rectTransform = gameObject.GetComponent<RectTransform>();
        }

        void Start(){
            
    }

        // Update is called once per frame
        void Update()
        {
            CheckForDetector();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            _rectTransform.anchoredPosition +=
                new Vector2(Direction.x * Speed * _speedMultiplier* Time.fixedDeltaTime, Direction.y * Speed * _speedMultiplier * Time.fixedDeltaTime);
            if (_rectTransform.anchoredPosition.x + _rectTransform.rect.width <= -10)
            {
                OnDestroyKey?.Invoke();
                Destroy(gameObject);
            }
        }

        private void CheckForDetector()
        {
            // var distanceXFromDetector = Vector2.Distance(_rectTransform.anchoredPosition, DetectorPosition) + DetectorWidth;
            
            if ( _rectTransform.anchoredPosition.x - _rectTransform.rect.width * 0.5 >= DetectorPosition.x + DetectorWidth * 0.5)
            {
                // key range right

            }else if (_rectTransform.anchoredPosition.x + _rectTransform.rect.width * 0.5 <= DetectorPosition.x - DetectorWidth * 0.5)
            {
                // key range left
                if (_hasExited) return;
                // Debug.Log("Exiting Detector");
                OnExitingDetection?.Invoke();
                _hasExited = true;
            }
            else
            {
                if(!_canPress) return;
                // Not In Range
                // Debug.Log("in Range");
                OnEnterDetection?.Invoke(_keyDirection, this.gameObject);
                _canPress = false;
            }
        }
        
    }
}
