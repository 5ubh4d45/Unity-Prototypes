using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _cameraPivot;

    [SerializeField] private float _zoomAmount = 4f;

    [SerializeField] private CinemachineVirtualCamera _cineCam;

    private Camera _mainCamera;

    private float _defaultZoom;
    private float _yRotation;

    // Start is called before the first frame update
    void Start()
    {
        if(_cineCam == null) _cineCam = GetComponentInChildren<CinemachineVirtualCamera>();
        _mainCamera = Camera.main;
        // _defaultZoom = _cineCam.m_Lens.FieldOfView;
        _defaultZoom = _mainCamera.fieldOfView;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetZoom();
        StartCoroutine(SetRotate(0.3f));
    }

    private void SetZoom()
    {
        // _cineCam.m_Lens.FieldOfView = _defaultZoom + _zoomAmount * UIManager.Instance.ZoomSlider;
        _mainCamera.fieldOfView = _defaultZoom + _zoomAmount * UIManager.Instance.ZoomSlider;
    }

    private IEnumerator SetRotate(float rotationTime)
    {
        var tempRotation = _cameraPivot.rotation.eulerAngles.y;
        var targetRotation = tempRotation + (float)UIManager.Instance.Rotattion;

        _cameraPivot.rotation = Quaternion.Euler(0,targetRotation,0);
        yield return null;
        var temptime = Time.time;

        // while (temptime + rotationTime > Time.time)
        // {
        //     _yRotation = Mathf.Lerp(tempRotation, targetRotation, Time.time / (temptime + rotationTime));
        //     _cameraPivot.rotation = Quaternion.Euler(0,_yRotation,0);
        //
        //     yield return new WaitForFixedUpdate();
        // }
    }
}
