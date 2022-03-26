using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CutOutSlots : MonoBehaviour, IDropHandler
{
    [SerializeField] private CutOutSlotData _slotData;
    
    [SerializeField] private Image _slotImage;

    private bool _hasRecivedPart;
    
    // Start is called before the first frame update
    void Start()
    {
        // _slotImage = GetComponentInChildren<Image>();
        _slotImage.sprite = _slotData.SlotSprite;
        // Debug.Log(_slotImage.color);
        _slotImage.color = _slotData.SlotColor;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("On Drop");
        if (_hasRecivedPart) return;
        
        var obj = eventData.pointerDrag;
        if (obj != null)
        {
            obj.TryGetComponent<DragglebleUIParts>(out var dragglebleUIParts);
            if (dragglebleUIParts != null || dragglebleUIParts.PartName != _slotData.PartName) return;
            // dragglebleUIParts.PartsCollected();
            _slotImage.color = dragglebleUIParts.CutoutImage.color;
            _hasRecivedPart = true;
            dragglebleUIParts.SetInSlot();
        }
    }
}

[System.Serializable]
public struct CutOutSlotData
{
    public string PartName;
    public Sprite SlotSprite;
    public Color SlotColor;
    [HideInInspector]
    public Color PartColor;
}
