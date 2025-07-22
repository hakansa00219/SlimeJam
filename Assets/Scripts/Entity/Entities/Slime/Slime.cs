using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Worker.Actions;

namespace Entity.Entities.Slime
{
    [RequireComponent(typeof(SlimeGathering))]
    public class Slime : MonoBehaviour, IGatherable
    {
        private EntitySpawner _entitySpawner;
        private SlimeGathering _slimeGathering;
        private Scriptable.Entities _entities;
        private int _gridPositionX;
        private int _gridPositionY;

        private void Awake()
        {
            _slimeGathering = GetComponent<SlimeGathering>();

            if (_slimeGathering != null)
            {
                _slimeGathering.Initialize(_entitySpawner, entities);
            }
            
            _entities = Res
        }

        public void Initialize(EntitySpawner spawner, int x, int y)
        {
            _entitySpawner = spawner;
            _gridPositionX = x;
            _gridPositionY = y;
            
        }
    }
}