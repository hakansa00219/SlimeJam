using Engine;
using Entity;
using Entity.Entities.Worker.Actions;
using UnityEngine;

namespace Structure
{
    public class Warehouse : MonoBehaviour, IStorage, IDepositable
    {
        public IStorage.StorageInfo CurrentInfo { get; set; } = new IStorage.StorageInfo(0, 0, 0, 0);

        public IStorage.StorageCapacity Capacity { get; set; } =
            new IStorage.StorageCapacity(int.MaxValue, 20, 20, 20, 20);

        public bool IsDeposited { get; set; }
        
        private Depositing _depositing;
        public TickActionBehaviour DepositingBehaviour() => _depositing;
        
        private void Awake()
        {
            _depositing = GetComponent<Depositing>();
        }
    }
}