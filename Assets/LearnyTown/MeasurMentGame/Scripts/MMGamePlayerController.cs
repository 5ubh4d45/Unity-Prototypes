using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LearnyTown.MeasurMentGame;
using UnityEngine;

public class MMGamePlayerController : MonoBehaviour
{
    [SerializeField] private float _scaleMultiplier = 0.2f;
    [SerializeField] private Transform _shuttle;
    
    [Space][SerializeField] private float _laneChangeTime;
    [SerializeField] private Transform _laneEntryTransform;


    private int _currentGap;

    private float _currentLanePosX;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        MmObstaclesManager.ObstacleRegistered += SetObstacle;
        MmButtonPanel.OnButtonPressed += SetMove;
    }
    private void OnDisable()
    {
        MmObstaclesManager.ObstacleRegistered -= SetObstacle;
        MmButtonPanel.OnButtonPressed -= SetMove;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetObstacle(int gap, float lanePosX)
    {
        _currentGap = gap;
        _currentLanePosX = lanePosX;
        Debug.Log($"Current Gap : {_currentGap}");
    }
    
    private void SetMove(int gapInput)
    {
        Debug.Log($"Input : {gapInput}");
        // transform.position = new Vector3(_laneEntryTransform.position.x, transform.position.y, transform.position.z);
        if (gapInput != _currentGap) {Debug.Log($"Incorrect"); return;}
        
        SetPlayerScale(gapInput);
        Debug.Log($"Correct");
        _shuttle.DOMove(new Vector3(_currentLanePosX, _shuttle.position.y, _shuttle.position.z), _laneChangeTime)
            .SetEase(Ease.InSine);
    }

    private void SetPlayerScale(int gapSize)
    {
        var scale = 1 + (_scaleMultiplier * gapSize);
        _shuttle.localScale = new Vector3(scale, scale, scale);
    }
}
