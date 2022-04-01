using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperUI : MonoBehaviour
{
    [SerializeField] private Image _paper;
    [SerializeField] private List<PaperSlot> _paperSlots;

    private static Color _paperColor;
    public static Color PaperColor => _paperColor;

    private void OnEnable()
    {
        PaperColorManager.OnSetPaperColor += SetColor;
    }

    private void OnDisable()
    {
        PaperColorManager.OnSetPaperColor -= SetColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetColor(Color paperColor, Color partColor)
    {
        _paperColor = paperColor;
        _paper.color = _paperColor;

        foreach (var paperSlot in _paperSlots)
        {
            paperSlot.SetPartColor(partColor);
        }
    }
    
}
