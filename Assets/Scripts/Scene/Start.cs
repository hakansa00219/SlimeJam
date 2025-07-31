using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class Start : MonoBehaviour
    {
        public void GameplayScene()
        {
            SceneManager.LoadScene(1);
        }
    }
}