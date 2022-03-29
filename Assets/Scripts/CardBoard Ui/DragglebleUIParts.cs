using System.Collections;
using System.Collections.Generic;
using Lean.Gui;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragglebleUIParts : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas _canvas;
    [SerializeField]private bool _inCutOutSlot = false;
    public bool InCutOutSlot => _inCutOutSlot;

    private string _partType;
    public string PartType => _partType;
    public Image CutoutImage { get; set; }

    private LeanDrag _leanDrag;
    private RectTransform _rectT;
    private RectTransform _slotRectT;
    private CanvasGroup _canvasGroup;
    private int _numberOfParts;
    
    // Start is called before the first frame update
    void Awake()
    {
        CutoutImage = GetComponent<Image>();
        _leanDrag = GetComponent<LeanDrag>();
        _rectT = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_inCutOutSlot) return;
        // Debug.Log("On Pointer Down");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_inCutOutSlot) return;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0.7f;
        // Debug.Log("On Begin Drag");
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (_inCutOutSlot) return;
        _rectT.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        // Debug.Log("On Drag");
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        // Debug.Log("On End Drag");
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1f;
        
        if (_inCutOutSlot) return;
        GotoSlotTransform(_slotRectT);
    }

    public void GotoSlotTransform(RectTransform targetRectTransform)
    {
        _slotRectT = targetRectTransform;
        _rectT.anchoredPosition = _slotRectT.anchoredPosition;
        // // var rect = _slotRectT.rect;
        _rectT.sizeDelta = _slotRectT.sizeDelta;
        // _rectT = _slotRectT;
    }

    public void SetInSlot()
    {
        _numberOfParts--;

        if (_numberOfParts <= 0)
        {
            _inCutOutSlot = true;
            gameObject.SetActive(false);
        }
    }

    public void SetPartName(string partName) { _partType = partName; }

    public void SetPartNumbers(int numbers) { _numberOfParts = numbers; }

    // public void PartsCollected() { _numberOfParts--; }
    
}
