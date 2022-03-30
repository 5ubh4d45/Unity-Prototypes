using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartingScreen : MonoBehaviour
{
    [SerializeField] private List<GameLevel> _gameLevels;

    [SerializeField] private Button _mainScreen;

    [SerializeField] private GameObject _levelScelection;


    private static int _currentGameLevelIndex;
    
    private void Awake()
    {
        foreach (var gameLevel in _gameLevels)
        {
            gameLevel.Button.image.sprite = gameLevel.ModelSprite;
            gameLevel.Button.onClick.AddListener(delegate
            {
                LoadNextScene(gameLevel.SceneIndex);
            });
        }
    }

    private void LoadNextScene(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        _currentGameLevelIndex = sceneIndex;
        _levelScelection.SetActive(false);
    }

    public void GoBackToMainScreen()
    {
        if (_currentGameLevelIndex == 0) return;
        SceneManager.UnloadSceneAsync(_currentGameLevelIndex);
        _levelScelection.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public struct GameLevel
{
    public String Name;
    public int SceneIndex;
    public Sprite ModelSprite;
    public Button Button;
}
