using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Engine
{
    public abstract class TickActionBehaviour : MonoBehaviour
    {
        protected abstract int TickDelay { get; }
        private int TickCounter = 0;
        
        [SerializeField]
        private Image progressBar;
        
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

            
            TickCounter++;
            if(progressBar != null)
                progressBar.fillAmount = (float)TickCounter / TickDelay;
            
            Debug.Log($"Tick counter started! {TickCounter} - " + System.DateTime.Now.ToString("HH:mm:ss.fff"));
            if (TickCounter != TickDelay) return;
            
            TickCounter = 0;
            OnTick();
            isActionDone = true;

            if (progressBar != null) 
                ProgressBarReset();
        }

        private async UniTaskVoid ProgressBarReset()
        {
            await TickSystem.WaitForNextTickAsync();
            if(progressBar != null)
                progressBar.fillAmount = 0;
        }

        protected abstract void OnTick();
    }
}