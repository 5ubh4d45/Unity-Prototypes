using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperSlot : MonoBehaviour
{
    [SerializeField] private string _slotName;
    [SerializeField] private int _numberOfParts = 1;
    // [SerializeField] private int _no
    [SerializeField] private Image _slotImage;
    [SerializeField] private DragglebleUIParts _draggleblePart;

    private Color _cutOutColor;
    
    void Start()
    {
        if (_slotImage ==null) _slotImage = GetComponent<Image>();
        if (_draggleblePart == null) _draggleblePart = GetComponent<DragglebleUIParts>();
        _draggleblePart.GotoSlotTransform(_slotImage.rectTransform);
        _draggleblePart.CutoutImage.sprite = _slotImage.sprite;
        _draggleblePart.SetPartName(_slotName);
        _draggleblePart.SetPartNumbers(_numberOfParts);
        // SetPartColor(_slotImage.color);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPartColor(Color color)
    {
        Debug.Log("Setting paper Cutout Color");
        if (_draggleblePart.InCutOutSlot) return;

        _cutOutColor = color;
        _draggleblePart.CutoutImage.color = _cutOutColor;
    }
}
