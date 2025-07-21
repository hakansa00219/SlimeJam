using Engine;
using UnityEngine;

namespace Worker
{
    public class WorkerMovement : TickActionBehaviour
    {
        protected override void OnTick()
        {
            Debug.Log("Move");
        }
    }
}