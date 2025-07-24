using Engine;
using Entity.Entities.Worker.Actions;
using UnityEngine;

namespace Entity.Entities.Tree
{
    public class WoodMaterial : MonoBehaviour, IMaterial
    {
        private PickingUp _woodPickingUp;
        public TickActionBehaviour PickingUpBehaviour() => _woodPickingUp;

        private void Awake()
        {
            _woodPickingUp = GetComponent<PickingUp>();
        }
        public void Pickup()
        {
            //Add sound
            Destroy(this.gameObject);
        }
    }
}