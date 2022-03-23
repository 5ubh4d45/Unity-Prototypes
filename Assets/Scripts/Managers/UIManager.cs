using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : StaticInstance<UIManager>
{
    [SerializeField] private Slider _slider;
    [SerializeField] private UIRotateButtonManager _rotateButtonManager;
    [SerializeField] private int _rotationStep;

    private float _zoomSlider;
    public float ZoomSlider => _zoomSlider;

    private int _rotation;
    public int Rotattion => _rotation;

    private bool _canRotate;
    public bool CanRotate => _canRotate;
    
    // private Vector2 _rotate;
    // public Vector2 Rotate => _rotate;
    // public static event Action Rotate;
    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        _slider.maxValue = 1f;
        _slider.minValue = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckZoom();
        SetRotate();
    }

    private void CheckZoom()
    {
        _zoomSlider = _slider.value;
    }

    public void ToggleRotate()
    {
        _canRotate = !_canRotate;
    }

    private void SetRotate()
    {
        _rotation = _rotateButtonManager.Rotation * _rotationStep;
    }
}
