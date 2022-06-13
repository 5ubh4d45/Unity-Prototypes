using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace LearnyTown.MissingNumberGame
{
    public class MnCarController : MonoBehaviour
    {

        // [SerializeField] private Vector3 _threeLaneDistance;
        [SerializeField]private float _laneChangeDuration;
        [SerializeField] private List<Renderer> _carRenderer;


        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        internal void ChangeLane(float laneNo)
        {
            // switch (laneNo)
            // {
            //     case 0:
            //         transform.DOLocalMoveX(_threeLaneDistance.x, _laneChangeDuration).SetEase(Ease.OutExpo);
            //         break;
            //     
            //     case 1:
            //         transform.DOLocalMoveX(_threeLaneDistance.y, _laneChangeDuration).SetEase(Ease.OutExpo);
            //         break;
            //     
            //     case 2:
            //         transform.DOLocalMoveX(_threeLaneDistance.z, _laneChangeDuration).SetEase(Ease.OutExpo);
            //         break;
            //     
            //     default :
            //         transform.DOLocalMoveX(_threeLaneDistance.y, _laneChangeDuration).SetEase(Ease.OutExpo);
            //         break;
            // }
            transform.DOLocalMoveX(laneNo, _laneChangeDuration).SetEase(Ease.OutExpo);
        }

        internal IEnumerator TakeDamage()
        {
            var ogColor = new List<Color>(_carRenderer.Count);
            // Debug.Log(_carRenderer.Count);
            for (int i = 0; i < _carRenderer.Count; i++)
            {
                ogColor.Add(_carRenderer[i].material.color);
                // ogMat[i].materials = _carRenderer[i].materials;
            }
            // var ogColor = ogMat.color;
            var wait1 = new WaitForSeconds(0.05f);
            var wait2 = new WaitForSeconds(0.1f);

            void ChangeColor(Color color)
            {
                foreach (var rend in _carRenderer)
                {
                    rend.material.color = color;
                }
            }
            
            ChangeColor(Color.white);
            yield return wait1;
            
            ChangeColor(Color.red);
            yield return wait2;
            
            ChangeColor(Color.white);
            yield return wait1;
            
            ChangeColor(Color.red);
            yield return wait2;
            
            ChangeColor(Color.white);
            yield return wait1;
            
            ChangeColor(Color.red);
            yield return wait2;


            for (var i = 0; i < _carRenderer.Count; i++)
            {
                _carRenderer[i].material.color = ogColor[i];
            }

            yield return null;
        }
    }
}
