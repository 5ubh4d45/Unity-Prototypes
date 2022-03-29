using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutOutPartsManager : MonoBehaviour
{
    [SerializeField] private GameObject _cutOutWinScreen;
    [SerializeField] private GameObject _gamePlayUI;
    [SerializeField] private DragHandler _dragHandler;
    
    [Space]
    [SerializeField] private List<CutOutSlots> _slotsList;

    // public static event Action<PartDetails> OnColorSelected;
    public static event Action<string, string, Color> OnColorSelected;

    private static int _completedPartsCount;
    private static int _totalPartsCount;
    private static bool _completedCutoutParts;
    
    // Start is called before the first frame update
    void Start()
    {
        _completedPartsCount = 0;
        _totalPartsCount = _slotsList.Count; //checking total parts
        _cutOutWinScreen.SetActive(false);
        gameObject.SetActive(false);
        _dragHandler.CanDrag = false;
    }

    private void OnEnable()
    {
        CutOutSlots.OnReceivedPart += HasReceivedParts;
    }

    private void OnDisable()
    {
        CutOutSlots.OnReceivedPart -= HasReceivedParts;
    }

    private void OnDestroy()
    {
        
    }

    private void HasReceivedParts(string partType, string partName, Color partColor)
    {
        _totalPartsCount = _slotsList.Count; //checking total parts
        _completedPartsCount++;
        
        OnColorSelected?.Invoke(partType, partName, partColor);
        // Debug.Log($"Type:{partType}, Name:{partName}, Color:{partColor}");
        if (_completedCutoutParts)
        {
            Debug.Log("All cutout Parts assembled");
            ShowUIWin();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_completedPartsCount >= _totalPartsCount)
        {
            _completedCutoutParts = true;
            // Debug.Log("All cutout Parts assembled");
            ShowUIWin();
        }
        else
        {
            _completedCutoutParts = false;
        }
    }

    private void ShowUIWin()
    {
        if (_completedCutoutParts)
        {
            _cutOutWinScreen.SetActive(true);
        }
    }

    public void ProceedToGamePlay()
    {
        _gamePlayUI.SetActive(true);
        gameObject.SetActive(false);
        _dragHandler.CanDrag = true;
    }
}

