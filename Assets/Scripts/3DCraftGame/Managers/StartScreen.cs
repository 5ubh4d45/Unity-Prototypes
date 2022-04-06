using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CraftGame;

namespace CraftGame
{
    public class StartScreen : MonoBehaviour
    {
        [SerializeField] private DragHandler _dragHandler;
        [SerializeField] private CutOutPartsManager _cutOutPartsManager;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartGame()
        {
            _dragHandler.CanDrag = false;
            _cutOutPartsManager.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
