using System;
using Engine;
using Entity.Entities.Worker.Actions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entity.Entities.Berry
{
    public class BerryMaterial : MonoBehaviour, IMaterial
    {
        private PickingUp _berryPickingUp;
        public TickActionBehaviour PickingUpBehaviour() => _berryPickingUp;

        private void Awake()
        {
            _berryPickingUp = GetComponent<PickingUp>();
        }

        public void Pickup()
        {
            //Add sound
            Destroy(this.gameObject);
        }
    }
}