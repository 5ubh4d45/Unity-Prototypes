using System;
using UnityEngine;
using UnityEngine.UI;

namespace LearnyTown.NumberMatchingGame
{
    public class NmUiManager : MonoBehaviour
    {

        [SerializeField] private Vector2Int _easyLevelGrid;
        [SerializeField] private float _easyCamDistanceOffsetY;
        [SerializeField] private Vector2Int _midLevelGrid;
        [SerializeField] private float _midCamDistanceOffsetY;
        [SerializeField] private Vector2Int _hardLevelGrid;
        [SerializeField] private float _hardCamDistanceOffsetY;

        [Space] [SerializeField] private Camera _camera;

        [Space] [SerializeField] private GameObject _startScreen;
        [SerializeField] private GameObject _gamePlayUI;
        [SerializeField] private Button _easyButton;
        [SerializeField] private Button _midButton;
        [SerializeField] private Button _hardButton;


        [Space] [SerializeField] private Button _retryButton;
        [SerializeField] private Button _exitButton;

        public static event Action<Vector2Int> OnLevelSelection;
        public static event Action OnLevelRetry;
        public static event Action OnLevelExit;


        private Vector3 _defaultCamPosition;


        private void OnEnable()
        {
            _easyButton.onClick.AddListener(()=> SetButtonClicks(_easyLevelGrid, _easyCamDistanceOffsetY));
            _midButton.onClick.AddListener(()=> SetButtonClicks(_midLevelGrid, _midCamDistanceOffsetY));
            _hardButton.onClick.AddListener(()=> SetButtonClicks(_hardLevelGrid, _hardCamDistanceOffsetY));
            
            _retryButton.onClick.AddListener((() => OnLevelRetry?.Invoke()));
            _exitButton.onClick.AddListener(JumpToMainMenu);
        }

        private void OnDisable()
        {
            _easyButton.onClick.RemoveAllListeners();
            _midButton.onClick.RemoveAllListeners();
            _hardButton.onClick.RemoveAllListeners();
            
            _retryButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }

        // Start is called before the first frame update
        void Start()
        {
            _defaultCamPosition = _camera.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void JumpToMainMenu()
        {
            OnLevelExit?.Invoke();
            _gamePlayUI.SetActive(false);
            _startScreen.SetActive(true);
        }

        private void SetButtonClicks(Vector2Int levelGrid, float camOffsetY)
        {

            _camera.transform.position = _defaultCamPosition + new Vector3(0, camOffsetY, 0);
            
            OnLevelSelection?.Invoke(levelGrid);

            _startScreen.SetActive(false);
            _gamePlayUI.SetActive(true);
        }
    }
}
