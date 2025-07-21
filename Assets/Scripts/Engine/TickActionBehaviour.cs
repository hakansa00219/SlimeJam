using UnityEngine;

namespace Engine
{
    public abstract class TickActionBehaviour : MonoBehaviour
    {
        protected abstract int TickDelay { get; }
        private int _tickCounter = 0;
        
        private void OnEnable()
        {
            TickSystem.Tick += OnTickAction;
        }
        
        private void OnDisable()
        {
            TickSystem.Tick -= OnTickAction;
        }
        
        private void OnTickAction()
        {
            _tickCounter++;
            if (_tickCounter != TickDelay) return;
            
            _tickCounter = 0;
            OnTick();
        }

        protected abstract void OnTick();
    }
}