using Engine;

namespace Entity
{
    public interface IMaterial
    {
        public TickActionBehaviour PickingUpBehaviour();
        public void Pickup();
    }
}