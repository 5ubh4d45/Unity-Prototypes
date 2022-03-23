using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPointReciver : MonoBehaviour
{
    private bool _canAttach = true;
    public bool CanAttach => _canAttach;

    private Collider _collider;
    
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("AttachPoint"))
    //     {
    //         Debug.Log("Found Attachment");
    //         _collider.enabled = false;
    //         _canAttach = false;
    //     }
    // }
}
