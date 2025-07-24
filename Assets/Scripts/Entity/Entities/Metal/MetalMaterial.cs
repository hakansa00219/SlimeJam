using Engine;
using Entity.Entities.Worker.Actions;
using UnityEngine;

namespace Entity.Entities.Metal
{
    public class MetalMaterial : MonoBehaviour, IMaterial
    {
        private PickingUp _metalPickingUp;
        public TickActionBehaviour PickingUpBehaviour() => _metalPickingUp;

        private void Awake()
        {
            _metalPickingUp = GetComponent<PickingUp>();
        }
        public void Pickup()
        {
            //Add sound
            Destroy(this.gameObject);
        }
    }
}