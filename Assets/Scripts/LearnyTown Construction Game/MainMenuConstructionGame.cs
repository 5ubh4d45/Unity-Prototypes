using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LearnyTown.ConstructionGame
{


    public class MainMenuConstructionGame : MonoBehaviour
    {
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _easyLevelSelection;
        [SerializeField] private GameObject _hardLevelSelection;
        [SerializeField] private GameObject _veryHardLevelSelection;


        // Start is called before the first frame update
        void Start()
        {
            GoToMainMenu();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void GoToMainMenu()
        {
            _mainMenu.SetActive(true);
            _easyLevelSelection.SetActive(false);
            _hardLevelSelection.SetActive(false);
            _veryHardLevelSelection.SetActive(false);
        }

        public void GoToEasyLevelSelection()
        {
            _mainMenu.SetActive(false);
            _easyLevelSelection.SetActive(true);
        }

        public void GoToHardLevelSelection()
        {
            _mainMenu.SetActive(false);
            _hardLevelSelection.SetActive(true);
        }

        public void GoToVeryHardLevelSelection()
        {
            _mainMenu.SetActive(false);
            _veryHardLevelSelection.SetActive(true);
        }
    }
}
