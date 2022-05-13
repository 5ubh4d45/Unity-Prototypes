using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class OeNumbers : MonoBehaviour
{

    [SerializeField] private TextMeshPro _textMesh;

    public static event Action<bool> OnNumberCollisionIsEven; 

    private bool _isEven;
    public bool IsEven => _isEven;


    private float _speed;
    
    // Start is called before the first frame update
    void Start()
    {
        SetNumber();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        KeepStraight();
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        OnNumberCollisionIsEven?.Invoke(_isEven);
        Destroy(gameObject);
        
        // Debug.Log("NumberCollided");
    }

    private void KeepStraight()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void SetNumber()
    {
        int tempNo = Random.Range(1, 100);
        // if (tempNo % 2 != 0)
        // {
        //     _isEven = false;
        // }
        // else
        // {
        //     _isEven = true;
        //     
        // }
        _isEven = (tempNo % 2 == 0);
        
        _textMesh.SetText($"{tempNo}");
    }

    public void SetStatus(float speed)
    {
        _speed = speed;
    }

    private void Move()
    {
        transform.position += new Vector3(0, 0, -(_speed * Time.deltaTime));
        if (transform.position.z < -20f)
        {
            Destroy(gameObject);
        }
    }
}
