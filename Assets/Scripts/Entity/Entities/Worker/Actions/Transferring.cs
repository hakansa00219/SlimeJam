using Engine;
using Structure;
using UnityEngine;

namespace Entity.Entities.Worker.Actions
{
    public class Transferring : TickActionBehaviour
    {
        protected override int TickDelay { get; } = 2;
        
        private IStorage _workerStorage;
        private IPurchasable _purchasable;
        private bool _isConfirmed = false;
        
        public void Initialize(IPurchasable purchasable, IStorage workerStorage)
        {
            _workerStorage = workerStorage;
            _purchasable = purchasable;
        }
        
        protected override void OnTick()
        {
            if (!_isConfirmed)
            {
                _isConfirmed = true;
                return;
            }
            
            IStorage.StorageInfo workerInfo = _workerStorage.CurrentInfo;
            IPurchasable.Cost cost = _purchasable.PurchaseCost;

            if (cost.Berry > 0 && workerInfo.Berry > 0)
            {
                cost.Berry -= 1;
                workerInfo.Berry -= 1;
                _workerStorage.CurrentInfo = workerInfo;
                _purchasable.TransferConditionCheck();
                return;
            }
            if (cost.Metal > 0 && workerInfo.Metal > 0)
            {
                cost.Metal -= 1;
                workerInfo.Metal -= 1;
                _workerStorage.CurrentInfo = workerInfo;
                _purchasable.TransferConditionCheck();
                return;
            }
            if (cost.Slime > 0 && workerInfo.Slime > 0)
            {
                cost.Slime -= 1;
                workerInfo.Slime -= 1;
                _workerStorage.CurrentInfo = workerInfo;
                _purchasable.TransferConditionCheck();
                return;
            }
            if (cost.Wood > 0 && workerInfo.Wood > 0)
            {
                cost.Wood -= 1;
                workerInfo.Wood -= 1;
                _workerStorage.CurrentInfo = workerInfo;
                _purchasable.TransferConditionCheck();
                return;
            }

            
            
            Debug.LogWarning("Worker has no materials to transfer or the cost is already fulfilled.");
            return;
        }
        
    }
}