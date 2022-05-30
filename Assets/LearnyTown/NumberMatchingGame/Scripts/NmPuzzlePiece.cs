using DG.Tweening;
using TMPro;
using UnityEngine;

namespace LearnyTown.NumberMatchingGame
{
    public class NmPuzzlePiece : MonoBehaviour
    {

        [SerializeField] private GameObject _lidObj;
        [SerializeField] private GameObject _bodyObj;
        [SerializeField] private TextMeshPro _numberText;

        [Space]
        [SerializeField] private Vector3 _targetLidPoint;
        [SerializeField] private float _puzzleOpenTime;
        
        
        private int _number;
        private bool _isOpen;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        internal void SetNumber(int targetNum)
        {
            _number = targetNum;
            _numberText.SetText(_number.ToString());
        }

        [ContextMenu("Open Puzzle")]
        internal void OpenPuzzle()
        {
            _lidObj.transform.DOLocalMove(_targetLidPoint, _puzzleOpenTime).SetEase(Ease.OutElastic)
                .OnComplete(() => _isOpen = true);
        }
        [ContextMenu("Close Puzzle")]
        internal void ClosePuzzle()
        {
            _lidObj.transform.DOLocalMove(new Vector3(0, 0, 0), _puzzleOpenTime).SetEase(Ease.OutElastic)
                .OnComplete(() => _isOpen = false);
        }

        internal void DeletePuzzle()
        {
            _bodyObj.GetComponent<Renderer>().material.DOFade(0f, 0.5f).SetEase(Ease.Linear)
                .OnComplete(() => Destroy(gameObject));
        }
    }
}
