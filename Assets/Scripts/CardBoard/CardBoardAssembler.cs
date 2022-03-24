using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoardAssembler : MonoBehaviour
{
    public AttachPoint[] AttachPoints;

    private static int _CompletedPartsCount;
    private static int _totalPartsCount;
    private static bool _CompletedAtttachingParts;
    public static bool CompletedAttachingParts => _CompletedAtttachingParts;

    // Start is called before the first frame update
    void Start()
    {
        HideParts();
        CheckCounts();
        // Debug.Log($"Total Parts: {_totalPartsCount} \n Completed Parts: {_CompletedPartsCount}");
    }

    // Update is called once per frame
    void Update()
    {
        if (_CompletedPartsCount >= _totalPartsCount)
        {
            // Debug.Log("Completed");
            _CompletedAtttachingParts = true;
        }
    }

    private void HideParts()
    {
        for (int i = 0; i < AttachPoints.Length; i++)
        {
            AttachPoints[i].AttachableMeshRenderer.enabled = false;
            AttachPoints[i].HoloGramMeshRenderer.gameObject.SetActive(true);
            AttachPoints[i].HoloGramMeshRenderer.enabled = false;
        }
    }

    private void CheckCounts()
    {
        for (int i = 0; i < AttachPoints.Length; i++)
        {
            _totalPartsCount++;
            
            if (AttachPoints[i].HasAttached)
            {
                _CompletedPartsCount++;
            }
        }
    }
    public void ShowPart(AttachPoint attachPoint)
    {
        attachPoint.AttachableMeshRenderer.enabled = true;
        attachPoint.HoloGramMeshRenderer.enabled = false;
        attachPoint.CanAttach = false;
        attachPoint.HasAttached = true;
        attachPoint.AttachCollider.enabled = false;
        _CompletedPartsCount++;
    }

    public IEnumerator ShowHoloGram(AttachPoint attachPoint)
    {
        // Debug.Log($"Showing HoloGram : {attachPoint.AttachCollider.name}");
        attachPoint.HoloGramMeshRenderer.enabled = true;
        yield return new WaitForSeconds(2f);
        HideHoloGram(attachPoint);
        
    }
    
    public void HideHoloGram(AttachPoint attachPoint)
    {
        // Debug.Log($"Hiding HoloGram : {attachPoint.AttachCollider.name}");
        attachPoint.HoloGramMeshRenderer.enabled = false;
    }
    
}

public interface IAssembler
{
    
}
[System.Serializable]
public class AttachPoint
{
    public string AttachableName;
    [SerializeField]
    private GameObject AtachableObject;
    public MeshRenderer HoloGramMeshRenderer;
    
    public MeshRenderer AttachableMeshRenderer => AtachableObject.GetComponent<MeshRenderer>();
    public Collider AttachCollider => AtachableObject.GetComponent<Collider>();
    public bool CanAttach;
    [field: HideInInspector]public bool HasAttached { get; set; }
}
[System.Serializable]
public class Attachable
{
    public string TargetPointname;
    public Transform AttachPoint;
    public LayerMask AttachMentLayerMask;
    public string TargetTag;
    public Vector3 DetectionBox;
    public Vector3 DetectionBoxOffset;
}
