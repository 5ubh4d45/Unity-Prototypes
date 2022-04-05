using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AdaptivePerformance;
using CraftGame;

namespace CraftGame
{
    public class DragAndDroppable : MonoBehaviour, IDraggable
    {
        [SerializeField] private Attachable _attachable;

        [SerializeField] private float _dectectionRadius;

        // [SerializeField] private LayerMask _attachMentLayerMask;
        // [SerializeField] private string _targetTag;
        // [SerializeField] private float _detectionRadius = 0.5f;
        // [SerializeField] private Vector3 _detecttionBoxHalfExtents;
        private Ray _ray;
        private RaycastHit _hit;
        private Camera _mainCamera;
        private bool _canCheck;
        private MeshRenderer _meshRenderer;

        // cached cardboard Assembler & Attachpoint
        private CardBoardAssembler _cBAss;
        private AttachPoint _attachPoint;
        private bool _canShow;

        // Start is called before the first frame update
        void Start()
        {
            // _attachable.AttachPoint = transform;
            _mainCamera = Camera.main;
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        void FixedUpdate()
        {
            // CheckForCollisions();
            CheckCollision2();
        }

        public void OnStartDrag()
        {
            // Debug.Log("Started Dragging");
            //
        }

        public void OnDragging()
        {
            // Debug.Log("Dragging");
            _canCheck = true;

        }

        public void OnEndDrag()
        {
            // Debug.Log("Stopped Dragging");

            _canCheck = false;
            if (_cBAss != null && _attachPoint != null && _canShow)
            {
                _cBAss.HideHoloGram(_attachPoint);
                _cBAss.ShowPart(_attachPoint, _meshRenderer.material);
                Debug.Log("SettingActive Limb");
                gameObject.SetActive(false);
            }

            // if (_cBAss != null && _attachPoint != null && !_canShow)
            // {
            //     _cBAss.HideHoloGram(_attachPoint);
            // }

        }


        private void CheckCollision2()
        {
            if (!_canCheck) return;
            Collider[] colliders = new Collider[1];
            // var size = Physics.OverlapSphereNonAlloc(transform.position, _detectionRadius, colliders, _attachMentLayerMask);
            Physics.OverlapBoxNonAlloc(_attachable.AttachPoint.position + _attachable.DetectionBoxOffset,
                _attachable.DetectionBox, colliders, Quaternion.identity,
                _attachable.AttachMentLayerMask);

            if (colliders[0] == null)
            {
                // _canShow = false;
                return;
            }

            var coll = colliders[0];
            var targetObject = coll.gameObject;
            // Debug.Log($"Target object: {targetObject.name}");

            if (targetObject.CompareTag(_attachable.TargetTag))
            {
                // Debug.Log($"{targetObject}");
                var cBAss = targetObject.GetComponentInParent<CardBoardAssembler>();
                if (cBAss != null)
                {
                    _canShow = true;
                    // Debug.Log($"{cBAss}");
                    foreach (var attachPoint in cBAss.AttachPoints)
                    {
                        if (attachPoint.AttachCollider.GetInstanceID() == coll.GetInstanceID() &&
                            attachPoint.CanAttach && attachPoint.AttachableType == _attachable.TargetPointType)
                        {
                            _cBAss = cBAss;
                            _attachPoint = attachPoint;

                            StartCoroutine(cBAss.ShowHoloGram(attachPoint)); // shows the hologram

                        }
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            // Gizmos.DrawWireSphere(transform.position, _detectionRadius);
            // Gizmos.DrawWireCube(transform.position, _attachable.DetectionBox);
            Gizmos.DrawWireCube(_attachable.AttachPoint.position + _attachable.DetectionBoxOffset,
                _attachable.DetectionBox);
            Gizmos.color = Color.yellow;
        }
    }
}
    
