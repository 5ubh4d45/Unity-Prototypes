using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBoardAssembler : MonoBehaviour
{
    public AttachPoint[] AttachPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckForCollision()
    {
        for (int i = 0; i < AttachPoints.Length; i++)
        {
            AttachPoints[i].CanAttach = AttachPoints[i].PointReciver.CanAttach;
            ShowParts();
        }
    }

    private void ShowParts()
    {
        for (int i = 0; i < AttachPoints.Length; i++)
        {
            if(!AttachPoints[i].CanAttach) return;
            
            if (!AttachPoints[i].HasAttached)
            {
                AttachPoints[i].AttachableGameObject.SetActive(true);
                Debug.Log("Enabling Atatchment!");
                AttachPoints[i].HasAttached = false;
            }
            else
            {
                AttachPoints[i].AttachableGameObject.SetActive(false);
            }
        }
    }
    
    
}

public interface IAssembler
{
    
}
[System.Serializable]
public class AttachPoint
{
    public string AttachPointName;
    public string AttachableName;
    public Collider AttachCollider;
    public AttachPointReciver PointReciver;
    public GameObject AttachableGameObject;
    public bool CanAttach;
    public bool HasAttached;
}
[System.Serializable]
public struct Attachable
{
    public string TargetPointname;
    public Transform AttachPoint;
}
