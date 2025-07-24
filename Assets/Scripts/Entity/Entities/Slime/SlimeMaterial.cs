using Engine;
using Entity.Entities.Worker.Actions;
using UnityEngine;

namespace Entity.Entities.Slime
{
    public class SlimeMaterial : MonoBehaviour, IMaterial
    {
        private PickingUp _slimePickingUp;
        public TickActionBehaviour PickingUpBehaviour() => _slimePickingUp;

        private void Awake()
        {
            _slimePickingUp = GetComponent<PickingUp>();
        }
        public void Pickup()
        {
            //Add sound
            Destroy(this.gameObject);
        }
    }
}