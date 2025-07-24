using Engine;
using Entity.Entities.Worker.Actions;

namespace Entity
{
    public interface IMaterial
    {
        public TickActionBehaviour PickingUpBehaviour();
        public void Pickup();
    }
}