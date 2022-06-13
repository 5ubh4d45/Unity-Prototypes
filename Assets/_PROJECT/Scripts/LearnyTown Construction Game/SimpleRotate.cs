using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace LearnyTown.ConstructionGame
{

    public class SimpleRotate : MonoBehaviour
    {
        [SerializeField]private Vector3 _rotationTarget;
        [SerializeField]private float _rotateDuration;

        // Start is called before the first frame update
        void Start()
        { 
            transform.DOLocalRotate(_rotationTarget, _rotateDuration, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDestroy()
        {
            DOTween.Kill(transform);
        }
    }
}
