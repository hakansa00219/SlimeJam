using System;
using Scriptable;
using UnityEngine;

namespace Cam
{
    public class InitialCameraLocation : MonoBehaviour
    {
        [SerializeField] private GridMap gridMap;

        private void Awake()
        {
            transform.position = new Vector3(gridMap.MainTile.X, gridMap.MainTile.Y, -10f);
        }
    }
}