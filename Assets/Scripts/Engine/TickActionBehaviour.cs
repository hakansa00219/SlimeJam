using UnityEngine;
using Worker.Actions;

namespace Engine
{
    public abstract class TickActionBehaviour : MonoBehaviour
    {
        protected abstract int TickDelay { get; }
        private int _tickCounter = 0;
        public bool isActive = false;
        public bool isActionDone = false;
        
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
            if (!isActive) return;

            
            
            _tickCounter++;
            Debug.Log($"Tick counter started! {_tickCounter} - " + System.DateTime.Now.ToString("HH:mm:ss.fff"));
            if (_tickCounter != TickDelay) return;
            
            _tickCounter = 0;
            OnTick();
            isActionDone = true;
        }

        private void LoopReset()
        {
            isActive = false;
            isActionDone = false;
        }

        protected abstract void OnTick();
    }
}