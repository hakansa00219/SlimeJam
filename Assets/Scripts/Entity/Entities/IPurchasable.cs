using Data;
using Engine;

namespace Entity.Entities
{
    public interface IPurchasable
    {
        public Cost PurchaseCost { get; set; }
        public bool IsPurchased { get; set; }
        public bool IsTransferred { get; set; }
        public void TransferConditionCheck();
        public TickActionBehaviour TransferringBehaviour();

        
    }
}