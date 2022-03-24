using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragHandler : MonoBehaviour
{
    [SerializeField] private float _dragPhysicsSpeed = 5f;
    [SerializeField] private float _dragSpeed = 0.1f;

    private InputManager _inputManager;
    
    private bool _isTouchPressed = false;
    private Vector3 _velocity;
    private Camera _mainCamera;
    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

    private void OnEnable()
    {
        _inputManager = InputManager.Instance;
        _inputManager.TouchPressed += OnTouchPressed;
        _inputManager.TouchReleased += OnTouchReleased;
    }

    private void OnDisable()
    {
        _inputManager.TouchReleased -= OnTouchPressed;
        _inputManager.TouchReleased -= OnTouchReleased;
    }

    private void Awake()
    {
        _mainCamera = Camera.main;
        _inputManager = InputManager.Instance;

    }

    void Start()
    {
        
    }

    
    void Update()
    {
        // Debug.Log($"DH TouchPos: {InputManager.Instance.TouchPosition}");

    }
    private void OnTouchPressed()
    {
        // Debug.Log("TouchPressed DH");
        _isTouchPressed = true;
        Ray ray = Camera.main.ScreenPointToRay(_inputManager.GetTouchPostion());
        Debug.DrawRay(ray.origin, ray.direction *10, Color.red); // debug ray
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Debug.Log(hit.collider.name);
            if (hit.collider != null && hit.collider.gameObject.GetComponent<IDraggable>() != null)
            {
                // Debug.Log($"Clicking object {hit.collider.name}");
                StartCoroutine(DragUpdate(hit.collider.gameObject));
            }
        }
    }
    
    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        float initialDistanceFromCamera =
            Vector3.Distance(clickedObject.transform.position, Camera.main.transform.position);
        clickedObject.TryGetComponent<Rigidbody>(out var rb);
        clickedObject.TryGetComponent<IDraggable>(out var iDragComponent);
        
        iDragComponent?.OnStartDrag();
        
        while (_isTouchPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(_inputManager.GetTouchPostion());
            Debug.DrawRay(ray.origin, ray.direction *10, Color.yellow); // debug ray
            if (rb != null)
            {
                // Debug.Log($"Clicking RB object in DragUpdate {rb.name}");
                Vector3 direction = ray.GetPoint(initialDistanceFromCamera) - clickedObject.transform.position;
                rb.velocity = direction * _dragPhysicsSpeed;
                
                iDragComponent?.OnDragging();
                yield return _waitForFixedUpdate;
            }
            else
            {
                clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, ray.GetPoint(initialDistanceFromCamera), ref _velocity, _dragSpeed);
                iDragComponent?.OnDragging();
                yield return null;
            }
        }
        iDragComponent?.OnEndDrag();
        
    }
    
    private void OnTouchReleased()
    {
        // Debug.Log("TouchReleased DH");
        _isTouchPressed = false;
    }
}
