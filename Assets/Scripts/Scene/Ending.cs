using UnityEngine;

namespace Scene
{
    public class Ending : MonoBehaviour
    {
        public static Ending Instance;
        
        [SerializeField] private GameObject endPanel;

        public GameObject EndPanel => endPanel;

        private void Awake()
        {
            Instance = this;
        }
    }
}