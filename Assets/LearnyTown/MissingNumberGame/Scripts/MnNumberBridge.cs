using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LearnyTown.MissingNumberGame
{
    public class MnNumberBridge : MonoBehaviour
    {
        [SerializeField] private List<TextMeshPro> _bridgeText;
        [SerializeField] private float[] _lanesPosX;
        [SerializeField] private float _respawnDistanceZ;
        [SerializeField] private float _dissapearDistanceZ;


        [Space] [SerializeField] private Transform _bridgeTransform;
        [SerializeField] private float _bridgeSpeed;
        [SerializeField] private Vector3 _bridgeRotation;
        [SerializeField] private float _bridgeDuration;

        internal static event Action<MnNumberBridge> OnPlayerArrival; 
        internal static event Action<MnNumberBridge> OnPlayerDeparture; 
        internal static float bridgeSpeed;

        private List<int> _ansList;
        internal List<int> ansList => _ansList;
        private int _correctTextIndex;

        internal float lanePosX;
        
        private int _correctAns;
        internal int correctAns => _correctAns;
        
        private bool _isBridgeOpen;
        private static bool canMove;

        internal bool isGameOver;

        // Start is called before the first frame update
        void Start()
        {
            canMove = true;
            SetBridge();
        }

        // Update is called once per frame
        void Update()
        {
            MoveBridge();
        }

        private void MoveBridge()
        {
            if (canMove)
            {
                transform.position -= new Vector3(0, 0, _bridgeSpeed * Time.deltaTime);
            }

            if (transform.position.z <= _dissapearDistanceZ)
            {
                transform.position = new Vector3(0, 0, _respawnDistanceZ);
                SetBridge();
            }
        }

        internal IEnumerator StopBridge(Vector3 targetStop, float waitCooldown, float snappingTime)
        {
            canMove = false;
            transform.DOMoveZ(targetStop.z, snappingTime).SetEase(Ease.OutExpo);

            yield return new WaitForSeconds(waitCooldown + 0.5f);
            canMove = !isGameOver;
        }

        private void OnTriggerEnter(Collider other)
        {
            OnPlayerArrival?.Invoke(this);
        }

        private void OnTriggerExit(Collider other)
        {
            OnPlayerDeparture?.Invoke(this);
        }


        [ContextMenu("Set Bridge")]
        internal void SetBridge()
        {
            // ClosBridge();
            LaneTextVisibility(true);

            int randBridgeIndex = Random.Range(0, 3);
            int randBridgeNumber = Random.Range(0, 100 - 2);

            // setting the text
            _ansList = new List<int>(3);
            for (int i = 0; i < 3; i++)
            {
                var textNumber = randBridgeNumber + i;
                _bridgeText[i].SetText(textNumber.ToString());
                _ansList.Add(textNumber);
            }

            // hiding the ans
            lanePosX = _lanesPosX[randBridgeIndex];
            _correctTextIndex = randBridgeIndex;
            _correctAns = randBridgeNumber + randBridgeIndex;
            // _bridgeText[_correctLane].gameObject.SetActive(false);
            SetBridgePosition(_correctTextIndex);
            // StartCoroutine(OpenBridge());
            StartCoroutine(ClosBridge());
        }

        [ContextMenu("Open Bridge")]
        internal IEnumerator OpenBridge()
        {
            _bridgeTransform.DOLocalRotate(_bridgeRotation, _bridgeDuration).SetEase(Ease.OutExpo);
                // .OnComplete(() => LaneTextVisibility(true));
            
            yield return new WaitForSeconds(_bridgeDuration);
            // resetting the ans
        }
        [ContextMenu("Close Bridge")]
        internal IEnumerator ClosBridge()
        {
            LaneTextVisibility(false);
            yield return new WaitForSeconds(1f);
            _bridgeTransform.DOLocalRotate(new Vector3(0, 0, 0), _bridgeDuration).SetEase(Ease.OutExpo);
            // yield return new WaitForSeconds(_bridgeDuration);
        }

        internal void LaneTextVisibility(bool choice)
        {
            _bridgeText[_correctTextIndex].gameObject.SetActive(choice);
        }
        
        private void SetBridgePosition(int bridgeNo)
        {
            _bridgeTransform.localPosition = new Vector3(_bridgeText[bridgeNo].transform.localPosition.x,
                _bridgeTransform.localPosition.y, _bridgeTransform.localPosition.z);
        }
    }
}