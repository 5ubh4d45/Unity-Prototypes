using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class DragAndDrop : MonoBehaviour, IDraggable
{
    [SerializeField] private Attachable _attachable;
    [SerializeField] private LayerMask _attachMentLayerMask;
    [SerializeField] private string _targetTag;
    [SerializeField]private float _detectionRadius = 0.5f;
    [SerializeField] private Vector3 _detecttionBoxHalfExtents;
    private Ray _ray;
    private RaycastHit _hit;
    private Camera _mainCamera;
    private bool _canCheck;

    // Start is called before the first frame update
    void Start()
    {
        _attachable.AttachPoint = transform;
        _mainCamera = Camera.main;
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
    }

    private void CheckForCollisions()
    {
        if (!_canCheck) return;

        if (Physics.SphereCast(transform.position, _detectionRadius, _mainCamera.transform.forward, out _hit, _attachMentLayerMask))
        {
            var targetObject = _hit.collider.gameObject;
            if (targetObject.CompareTag(_targetTag))
            {
                // Debug.Log($"{targetObject}");
                var cBAss = targetObject.GetComponentInParent<CardBoardAssembler>();
                if (cBAss != null)
                {
                    // Debug.Log($"{cBAss}");
                    foreach (var attachPoint in cBAss.AttachPoints)
                    {
                        if (attachPoint.AttachCollider.GetInstanceID() == _hit.collider.GetInstanceID() &&
                            attachPoint.CanAttach)
                        {
                            attachPoint.AttachableGameObject.SetActive(true);
                            attachPoint.CanAttach = false;
                            _hit.collider.enabled = false;
                            Debug.Log("SettingActive Limb");
                            gameObject.SetActive(false);
                        }
                    }

                }
            }
        }
    }

    private void CheckCollision2()
    {
        if (!_canCheck) return;
        Collider[] colliders = new Collider[1];
        // var size = Physics.OverlapSphereNonAlloc(transform.position, _detectionRadius, colliders, _attachMentLayerMask);
        Physics.OverlapBoxNonAlloc(transform.position, _detecttionBoxHalfExtents, colliders, Quaternion.identity,
            _attachMentLayerMask);
        
        if (colliders[0] == null) return;
        
        var coll = colliders[0];
        var targetObject = coll.gameObject;
        // Debug.Log($"{targetObject.name}");
        if (targetObject.CompareTag(_targetTag))
        {
            // Debug.Log($"{targetObject}");
            var cBAss = targetObject.GetComponentInParent<CardBoardAssembler>();
            if (cBAss != null)
            {
                // Debug.Log($"{cBAss}");
                foreach (var attachPoint in cBAss.AttachPoints)
                {
                    if (attachPoint.AttachCollider.GetInstanceID() == coll.GetInstanceID() &&
                        attachPoint.CanAttach)
                    {
                        attachPoint.AttachableGameObject.SetActive(true);
                        attachPoint.CanAttach = false;
                        coll.enabled = false;
                        Debug.Log("SettingActive Limb");
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Gizmos.DrawWireSphere(transform.position, _detectionRadius);
        Gizmos.DrawWireCube(transform.position, _detecttionBoxHalfExtents);
        Gizmos.color = Color.yellow;
    }
}
    
