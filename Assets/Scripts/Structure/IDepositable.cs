using Engine;

namespace Structure
{
    public interface IDepositable
    {
        public bool IsDeposited { get; set; }
        public TickActionBehaviour DepositingBehaviour();
        
    }
}