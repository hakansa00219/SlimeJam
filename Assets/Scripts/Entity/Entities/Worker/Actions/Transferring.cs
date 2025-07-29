using Data;
using Engine;
using Structure;
using UnityEngine;

namespace Entity.Entities.Worker.Actions
{
    public class Transferring : TickActionBehaviour
    {
        protected override int TickDelay { get; set; } = 2;
        
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
            Cost cost = _purchasable.PurchaseCost;

            if (cost.berry > 0 && workerInfo.Berry > 0)
            {
                cost.berry -= 1;
                workerInfo.Berry -= 1;
                _workerStorage.CurrentInfo = workerInfo;
                _purchasable.TransferConditionCheck();
                return;
            }
            if (cost.metal > 0 && workerInfo.Metal > 0)
            {
                cost.metal -= 1;
                workerInfo.Metal -= 1;
                _workerStorage.CurrentInfo = workerInfo;
                _purchasable.TransferConditionCheck();
                return;
            }
            if (cost.slime > 0 && workerInfo.Slime > 0)
            {
                cost.slime -= 1;
                workerInfo.Slime -= 1;
                _workerStorage.CurrentInfo = workerInfo;
                _purchasable.TransferConditionCheck();
                return;
            }
            if (cost.wood > 0 && workerInfo.Wood > 0)
            {
                cost.wood -= 1;
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