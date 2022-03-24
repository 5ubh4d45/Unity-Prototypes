using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InputManager : GenericSingletonClass<InputManager>
{
    public event Action TouchPressed;
    public event Action TouchReleased;

    
    // private Vector2 _touchPosition;

    private bool _isTouchPressed = false;
    private PlayerActions _playerActions;

    protected override void Awake()
    {
        base.Awake();
        _playerActions = new PlayerActions();
    }
    
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _playerActions.Enable();
        _playerActions.PlayerActionMap.PrimaryContact.started += OnTouchPressed;
        _playerActions.PlayerActionMap.PrimaryContact.canceled += OnTouchReleased;
    }

    private void OnDestroy()
    {
        _playerActions.PlayerActionMap.PrimaryContact.started -= OnTouchPressed;
        _playerActions.PlayerActionMap.PrimaryContact.canceled -= OnTouchReleased;
        _playerActions.Disable();
    }


    private void OnTouchPressed(InputAction.CallbackContext obj)
    {
        _isTouchPressed = true;
        // Debug.Log("TouchPressed IP");
        TouchPressed?.Invoke();
    }
    private void OnTouchReleased(InputAction.CallbackContext obj)
    {
        _isTouchPressed = false;
        // Debug.Log("TouchReleased IP");
        TouchReleased?.Invoke();
    }
    

    #region Redundant
    
    // private void OnTouchPressed(InputAction.CallbackContext context)
    // {
    //     _isTouchPressed = true;
    //     _dragHandler.OnTouchPressed();
    //     // Ray ray = _mainCamera.ScreenPointToRay(_playerActions.PlayerActionMap.PrimaryPosition.ReadValue<Vector2>());
    //     // RaycastHit hit;
    //     // Debug.DrawRay(ray.origin, ray.direction *10, Color.red);
    //     // if (Physics.Raycast(ray, out hit))
    //     // {
    //     //     if (hit.collider != null && hit.collider.gameObject.GetComponent<IDraggable>() != null)
    //     //     {
    //     //         // Debug.Log($"Clicking object {hit.collider.name}");
    //     //         StartCoroutine(DragUpdate(hit.collider.gameObject));
    //     //     }
    //     // }
    // }
    
    // private IEnumerator DragUpdate(GameObject clickedObject)
    // {
    //     float initialDistanceFromCamera =
    //         Vector3.Distance(clickedObject.transform.position, _mainCamera.transform.position);
    //     clickedObject.TryGetComponent<Rigidbody>(out var rb);
    //     clickedObject.TryGetComponent<IDraggable>(out var iDragComponent);
    //     iDragComponent?.OnStartDrag();
    //     while (_isTouchPressed)
    //     {
    //         Ray ray = _mainCamera.ScreenPointToRay(_playerActions.PlayerActionMap.PrimaryPosition.ReadValue<Vector2>());
    //         Debug.DrawRay(ray.origin, ray.direction *10, Color.yellow);
    //         if (rb != null)
    //         {
    //             // Debug.Log($"Clicking object {rb.name}");
    //             Vector3 direction = ray.GetPoint(initialDistanceFromCamera) - clickedObject.transform.position;
    //             rb.velocity = direction * _dragPhysicsSpeed;
    //             iDragComponent?.OnDragging();
    //             yield return _waitForFixedUpdate;
    //         }
    //         else
    //         {
    //             clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, ray.GetPoint(initialDistanceFromCamera), ref _velocity, _dragSpeed);
    //             iDragComponent?.OnDragging();
    //             yield return null;
    //         }
    //     }
    //     iDragComponent?.OnEndDrag();
    // }
    
    // private void OnTouchReleased(InputAction.CallbackContext obj)
    // {
    //     _isTouchPressed = false;
    //     _dragHandler.OnTouchReleased();
    //     // Debug.Log($"Touch released");
    // }
    #endregion

    void Update()
    {
        // Debug.Log($"IP TouchPos: {_playerActions.PlayerActionMap.PrimaryPosition.ReadValue<Vector2>()}");
    }
    

    public Vector2 GetTouchPostion()
    {
        return _playerActions.PlayerActionMap.PrimaryPosition.ReadValue<Vector2>();
    }
}
