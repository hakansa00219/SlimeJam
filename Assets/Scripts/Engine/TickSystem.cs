using System;
using UnityEngine;

namespace Engine
{
    public class TickSystem : MonoBehaviour
    {
        public static event Action Tick;

        private const float TICK_LENGTH = 0.6f;
        private float _estimatedTime = 0;
        private void Update()
        {
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
            Debug.Log("Tick");
        }
    }
}