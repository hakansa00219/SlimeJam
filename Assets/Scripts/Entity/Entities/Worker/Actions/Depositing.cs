using Engine;
using Structure;
using UnityEngine;

namespace Entity.Entities.Worker.Actions
{
    public class Depositing : TickActionBehaviour
    {
        protected override int TickDelay { get; } = 2;

        private IStorage _warehouseStorage;
        private IDepositable _depositable;
        private IStorage _workerStorage;
        
        public void Initialize(IStorage workerStorage, IStorage warehouseStorage, IDepositable depositable)
        {
            _workerStorage = workerStorage;
            _warehouseStorage = warehouseStorage;
            _depositable = depositable;
        }
        
        protected override void OnTick()
        {
            IStorage.StorageInfo warehouseInfo = _warehouseStorage.CurrentInfo;
            IStorage.StorageInfo workerInfo = _workerStorage.CurrentInfo;

            if (workerInfo.Berry > 0 && _warehouseStorage.CurrentInfo.Berry < _warehouseStorage.Capacity.Berry)
            {
                warehouseInfo.Berry += 1;
                workerInfo.Berry -= 1;
            }
            else if (workerInfo.Metal > 0 && _warehouseStorage.CurrentInfo.Metal < _warehouseStorage.Capacity.Metal)
            {
                warehouseInfo.Metal += 1;
                workerInfo.Metal -= 1;
            }
            else if (workerInfo.Slime > 0 && _warehouseStorage.CurrentInfo.Slime < _warehouseStorage.Capacity.Slime)
            {
                warehouseInfo.Slime += 1;
                workerInfo.Slime -= 1;
            }
            else if (workerInfo.Wood > 0 && _warehouseStorage.CurrentInfo.Wood < _warehouseStorage.Capacity.Wood)
            {
                warehouseInfo.Wood += 1;
                workerInfo.Wood -= 1;
            }
            else
            {
                Debug.LogWarning("No space in warehouse or no materials to deposit.");
                return;
            }
            
            _warehouseStorage.CurrentInfo = warehouseInfo;
            _workerStorage.CurrentInfo = workerInfo;
                
            _depositable.IsDeposited = true;
        }
    }
}