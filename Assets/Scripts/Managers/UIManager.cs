using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Input Related")]
    [SerializeField] private Slider _slider;
    [SerializeField] private UIRotateButtonManager _rotateButtonManager;
    [SerializeField] private int _rotationStep;

    [Space] [Header("Screen Related")]
    [SerializeField] private TextMeshProUGUI _winScreenText;

    private float _zoomSlider;
    public float ZoomSlider => _zoomSlider;

    private int _rotation;
    public int Rotattion => _rotation;

    private bool _canRotate;
    public bool CanRotate => _canRotate;
    
    // private Vector2 _rotate;
    // public Vector2 Rotate => _rotate;
    // public static event Action Rotate;

    // Start is called before the first frame update
    void Start()
    {
        _slider.maxValue = 1f;
        _slider.minValue = 0f;
    }

    private void OnEnable()
    {
        Initiate();
    }

    // Update is called once per frame
    void Update()
    {
        CheckZoom();
        SetRotate();
        
        if(CardBoardAssembler.CompletedAttachingParts) ShowWinScreen();
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

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Initiate()
    {
        _winScreenText.gameObject.SetActive(false);
        _slider.enabled = true;
    }

    private void ShowWinScreen()
    {
        _winScreenText.gameObject.SetActive(transform);
        _slider.enabled = false;
    }
}
