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
        private bool _isTransferring;
        private IPurchasable.Cost _needs;
        
        public void Initialize(IStorage workerStorage, IStorage warehouseStorage, IDepositable depositable, bool isTransferring, IPurchasable.Cost needs = null)
        {
            _workerStorage = workerStorage;
            _warehouseStorage = warehouseStorage;
            _depositable = depositable;
            _isTransferring = isTransferring;
            _needs = needs;
        }
        
        protected override void OnTick()
        {
            IStorage.StorageInfo warehouseInfo = _warehouseStorage.CurrentInfo;
            IStorage.StorageInfo workerInfo = _workerStorage.CurrentInfo;

            if (!_isTransferring)
            {
                if (!Deposite(ref warehouseInfo, ref workerInfo)) 
                    return;
            }
            else
            {
                if(!Transfer(ref warehouseInfo, ref workerInfo, _needs))
                    return;
            }
            _warehouseStorage.CurrentInfo = warehouseInfo;
            _workerStorage.CurrentInfo = workerInfo;
                
            _depositable.IsDeposited = true;
        }

        private bool Deposite(ref IStorage.StorageInfo warehouseInfo, ref IStorage.StorageInfo workerInfo)
        {
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
                return false;
            }

            return true;
        }

        private bool Transfer(ref IStorage.StorageInfo warehouseInfo, ref IStorage.StorageInfo workerInfo, IPurchasable.Cost needs)
        {
            if (needs.Berry > 0 && warehouseInfo.Berry > 0 && workerInfo.Berry < _workerStorage.Capacity.Berry)
            {
                warehouseInfo.Berry -= 1;
                workerInfo.Berry += 1;
            }
            else if (needs.Metal > 0 && warehouseInfo.Metal > 0 && workerInfo.Metal < _workerStorage.Capacity.Metal)
            {
                warehouseInfo.Metal -= 1;
                workerInfo.Metal += 1;
            }
            else if (needs.Slime > 0 && warehouseInfo.Slime > 0 && workerInfo.Slime < _workerStorage.Capacity.Slime)
            {
                warehouseInfo.Slime -= 1;
                workerInfo.Slime += 1;
            }
            else if (needs.Wood > 0 && warehouseInfo.Wood > 0 && workerInfo.Wood < _workerStorage.Capacity.Wood)
            {
                warehouseInfo.Wood -= 1;
                workerInfo.Wood += 1;
            }
            else
            {
                Debug.LogWarning("No space in warehouse or no materials to transfer.");
                return false;
            }

            return true;
        }
    }
}