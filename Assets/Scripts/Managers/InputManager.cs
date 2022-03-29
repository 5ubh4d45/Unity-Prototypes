using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InputManager : GenericSingletonClass<InputManager>
{
    public static event Action TouchPressed;
    public static event Action TouchReleased;

    
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
        _playerActions = new PlayerActions();
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
    

    void Update()
    {
        // Debug.Log($"IP TouchPos: {_playerActions.PlayerActionMap.PrimaryPosition.ReadValue<Vector2>()}");
    }
    

    public Vector2 GetTouchPostion()
    {
        return _playerActions.PlayerActionMap.PrimaryPosition.ReadValue<Vector2>();
    }
}
