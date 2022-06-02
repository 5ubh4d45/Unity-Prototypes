using System.Collections;
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
        internal bool isOpen;
        
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

        internal int GetNumber()
        {
            return _number;
        }

        [ContextMenu("Open Puzzle")]
        internal IEnumerator OpenPuzzle()
        {
            _lidObj.transform.DOLocalMove(_targetLidPoint, _puzzleOpenTime).SetEase(Ease.OutElastic)
                .OnComplete(() => isOpen = true);
            yield return new WaitForSeconds(_puzzleOpenTime);

        }
        [ContextMenu("Close Puzzle")]
        internal IEnumerator ClosePuzzle()
        {
            _lidObj.transform.DOLocalMove(new Vector3(0, 0, 0), _puzzleOpenTime).SetEase(Ease.OutElastic)
                .OnComplete(() => isOpen = false);
            yield return new WaitForSeconds(_puzzleOpenTime);
        }

        internal void DeletePuzzle()
        {
            _bodyObj.GetComponent<Renderer>().material.DOFade(0f, 0.5f).SetEase(Ease.Linear)
                .OnComplete(() => Destroy(gameObject));
        }
    }
}
