using Engine;
using Entity;
using Entity.Entities;

namespace Structure
{
    public interface IDepositable
    {
        public bool IsDeposited { get; set; }
        public TickActionBehaviour DepositingBehaviour();
        public void Initialize(IStorage workerStorage, IPurchasable.Cost needs = null);
        
    }
}