using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class End : MonoBehaviour
    {
        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0) || UnityEngine.Input.GetMouseButtonDown(1))
            {
                StartScene();
            } 
        }


        private void StartScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}