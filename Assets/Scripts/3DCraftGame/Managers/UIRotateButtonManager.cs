using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CraftGame;

namespace CraftGame
{
    public class UIRotateButtonManager : MonoBehaviour
    {
        private int _rotation;
        public int Rotation => _rotation;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            _rotation = 0;
        }

        public void SetRotationLeft()
        {
            _rotation = 1;
        }

        public void SetRotationRight()
        {
            _rotation = -1;
        }
    }
}
