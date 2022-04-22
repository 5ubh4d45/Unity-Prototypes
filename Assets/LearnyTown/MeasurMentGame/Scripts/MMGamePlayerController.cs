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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        MmObstaclesManager.ObstacleRegistered += SetMove;
    }
    private void OnDisable()
    {
        MmObstaclesManager.ObstacleRegistered -= SetMove;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetMove(int gap, float lanePosX)
    {
        // transform.position = new Vector3(_laneEntryTransform.position.x, transform.position.y, transform.position.z);
        SetPlayerScale(gap);
        
        _shuttle.DOMove(new Vector3(lanePosX, _shuttle.position.y, _shuttle.position.z), _laneChangeTime)
            .SetEase(Ease.InSine);
    }

    private void SetPlayerScale(int gapSize)
    {
        var scale = 1 + (_scaleMultiplier * gapSize);
        _shuttle.localScale = new Vector3(scale, scale, scale);
    }
}
