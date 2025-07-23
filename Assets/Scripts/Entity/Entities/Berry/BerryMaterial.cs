using UnityEngine;

namespace Entity.Entities.Berry
{
    public class BerryMaterial : MonoBehaviour, IMaterial
    {
        public void Pickup()
        {
            //Add sound
            Destroy(this.gameObject);
        }
    }
}