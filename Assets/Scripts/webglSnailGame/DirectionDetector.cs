using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WebGLSnailGame;

namespace WebGLSnailGame
{

    public class DirectionDetector : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreTMPro;

        public static event Action<KeyDirection> OnCorrectKey;
        public static event Action<KeyDirection> OnIncorrectKey;

        private int _score;

        private KeyDirection _currentKeyDirection;
        private RectTransform _rectTransform;

        private bool _canTakeValue;
        private GameObject _cachedKeyObj;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        // Start is called before the first frame update
        void Start()
        {
            DirectionKeyUI.DetectorPosition = _rectTransform.anchoredPosition;
            DirectionKeyUI.DetectorWidth = _rectTransform.rect.width;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnEnable()
        {
            DirectionKeyUI.OnEnterDetection += UpdateCurrentKey;
            DirectionKeyUI.OnExitingDetection += ResetKeyDirection;
            SnailGameUIManager.OnDirectionInput += CheckInputs;
        }

        private void OnDisable()
        {
            DirectionKeyUI.OnEnterDetection -= UpdateCurrentKey;
            DirectionKeyUI.OnExitingDetection -= ResetKeyDirection;
            SnailGameUIManager.OnDirectionInput -= CheckInputs;
        }

        private void ResetKeyDirection()
        {
            _canTakeValue = true;
            _currentKeyDirection = KeyDirection.None;
            _cachedKeyObj = null;
            // Debug.Log($"Reset Key: {_currentKeyDirection}");
        }

        private void UpdateCurrentKey(KeyDirection keyDirection, GameObject cachedKeyObj)
        {
            _canTakeValue = true;
            
            _currentKeyDirection = keyDirection;
            _cachedKeyObj = cachedKeyObj;
            // Debug.Log($"Current Key: {_currentKeyDirection}");
        }

        private void CheckInputs(KeyDirection keyDirection)
        {
            if (!_canTakeValue) return;
            if (_currentKeyDirection == keyDirection)
            {
                // Debug.Log($"POP : {_currentKeyDirection}");
                OnCorrectKey?.Invoke(keyDirection);
                _canTakeValue = false;

                _score += 100;
                _scoreTMPro.text = "Score: " + _score.ToString();

                if (_cachedKeyObj != null)
                {
                    _cachedKeyObj.GetComponent<Image>().color = Color.green;
                }
                
            }
            else
            {
                OnIncorrectKey?.Invoke(keyDirection);
                _canTakeValue = false;
                _score -= 50;
                _scoreTMPro.text = "Score: " + _score.ToString();
                if (_cachedKeyObj != null)
                {
                    _cachedKeyObj.GetComponent<Image>().color = Color.red;
                }
            }
        }
        
    }
}
