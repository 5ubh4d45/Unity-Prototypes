using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using CraftGame;

namespace CraftGame
{
    public class CutOutSlots : MonoBehaviour, IDropHandler
    {
        [SerializeField] private CutOutSlotData _slotData;

        public CutOutSlotData Slotdata => _slotData;

        [SerializeField] private Image _slotImage;

        public static event Action<string, string, Color> OnReceivedPart;

        public CutOutSlotData CutOutSlotData => _slotData;

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
                if (dragglebleUIParts == null) return;

                if (dragglebleUIParts.PartType != _slotData.PartType) return;
                // dragglebleUIParts.PartsCollected();
                _slotImage.color = dragglebleUIParts.CutoutImage.color;
                _slotData.Color = dragglebleUIParts.CutoutImage.color;
                _hasRecivedPart = true;
                OnReceivedPart?.Invoke(_slotData.PartType, _slotData.PartName, _slotData.Color);
                dragglebleUIParts.SetInSlot();
            }
        }
    }

    [System.Serializable]
    public struct CutOutSlotData
    {
        public string PartType;
        public string PartName;
        public Sprite SlotSprite;
        public Color SlotColor;

        [HideInInspector] public Color Color;
    }
}
