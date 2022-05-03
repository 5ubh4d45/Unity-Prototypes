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
    [SerializeField] private Renderer _shuttleRenderer;
    [SerializeField] private LayerMask _obstacleLayer;
    
    [Space][SerializeField] private float _laneChangeTime;
    [SerializeField] private Transform _laneEntryTransform;

    

    private int _currentGap;
    private bool _hasCollided;
    private Collider[] _collider = new Collider[1];

    private MmObstacles _currentObstacle;
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
        // CheckForObstacle();
    }

    private void SetObstacle(int gap, float lanePosX, MmObstacles obstacle)
    {
        _currentGap = gap;
        _currentLanePosX = lanePosX;
        _currentObstacle = obstacle;
        Debug.Log($"Current Gap : {_currentGap}");
    }
    
    private void SetMove(int gapInput)
    {
        Debug.Log($"Input : {gapInput}");
        // transform.position = new Vector3(_laneEntryTransform.position.x, transform.position.y, transform.position.z);
        if (gapInput != _currentGap)
        {
            Debug.Log($"Incorrect");
            FlashEffect(Color.red, 3);
            return;
        }
        
        if (gapInput == _currentGap)
        {
            // set score
            
            Debug.Log($"Correct");
            
            // Debug.Log("Score: +10");

            _currentObstacle.GotCorrectAns = true;
            
            SetPlayerScale(gapInput);
            FlashEffect(Color.green, 3);
            SetPlayerLane(_currentLanePosX);
        }
        
        CheckForObstacle();

    }

    private void SetPlayerLane(float lanePosX)
    {
        _shuttle.DOMove(new Vector3(lanePosX, _shuttle.position.y, _shuttle.position.z), _laneChangeTime)
            .SetEase(Ease.InSine);
    }

    public void SetPlayerScale(int gapSize)
    {
        var scale = 1 + (_scaleMultiplier * gapSize);
        
        _shuttle.localScale = new Vector3(scale, scale, scale);
        // _shuttleRenderer.material.color = color;
    }

    public void FlashEffect(Color flashColor, int flashNumber)
    {
        if (flashNumber <= 0) flashNumber = 1;

        var material = _shuttleRenderer.material;
        var color = material.color;
        material.DOColor(flashColor, 0.1f).SetEase(Ease.Flash)
            .SetLoops(flashNumber, LoopType.Restart).OnComplete(() => material.color = color);
    }
    
    private void CheckForObstacle()
    {
        // if(_currentObstacle is null) return;
        //
        // if (_currentObstacle.HasProperlyCrossedPlayer)
        // {
        //     Debug.Log($"Cleared obstacle");
        //     return;
        // }
        //
        // if (_currentObstacle.BehindPlayer && !_currentObstacle.HasProperlyCrossedPlayer)
        // {
        //     Debug.Log($"couldn't clear obstacles");
        //     Debug.Log("Score: -10");
        //
        // }
        
        // Physics
        
    }
}
