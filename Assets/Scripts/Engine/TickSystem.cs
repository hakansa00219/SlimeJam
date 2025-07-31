using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Engine
{
    public class TickSystem : MonoBehaviour
    {
        public static event Action Tick;

        private const float TICK_LENGTH = 0.3f;
        private float _estimatedTime = 0;

        private bool _firstTick = false;
        
        private void Update()
        {
            if(!_firstTick)
            {
                Debug.Log($"Tick system started! {System.DateTime.Now:HH:mm:ss.fff}");
                _firstTick = true;
            }
            
            _estimatedTime += Time.deltaTime;

            if (!(_estimatedTime >= TICK_LENGTH)) return;
            
            Tick?.Invoke();
            _estimatedTime -= TICK_LENGTH;
        }

        private void OnEnable()
        {
            Tick += TestLog;
        }

        private void OnDisable()
        {
            Tick -= TestLog;
        }

        private void TestLog()
        {
            Debug.Log("Tick " + System.DateTime.Now.ToString("HH:mm:ss.fff"));
        }
        
        public static UniTask WaitForNextTickAsync()
        {
            var tcs = new UniTaskCompletionSource();

            void OnTick()
            {
                Tick -= OnTick;
                tcs.TrySetResult();
            }

            Tick += OnTick;
            return tcs.Task;
        }
    }
}