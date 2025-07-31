using System;
using UnityEngine;

namespace Scene
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private GameObject tutorialPanel;
        private void Awake()
        {
            Time.timeScale = 0;
        }


        public void ContinueTheGame()
        {
            tutorialPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}