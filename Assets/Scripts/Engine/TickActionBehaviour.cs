using UnityEngine;

namespace Engine
{
    public abstract class TickActionBehaviour : MonoBehaviour
    {
        private void OnEnable()
        {
            TickSystem.Tick += OnTick;
        }
        
        private void OnDisable()
        {
            TickSystem.Tick -= OnTick;
        }

        protected abstract void OnTick();
    }
}