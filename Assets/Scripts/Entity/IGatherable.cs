using Engine;

namespace Entity
{
    public interface IGatherable
    {
        public void Initialize(EntitySpawner spawner, int x, int y);
        public TickActionBehaviour GatheringBehaviour();
    }
}