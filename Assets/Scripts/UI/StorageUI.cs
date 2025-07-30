using Cysharp.Threading.Tasks;
using Data;
using Engine;
using Entity.Entities;
using Scriptable;
using Structure;
using UnityEngine;

namespace UI
{
    public class StorageUI : MonoBehaviour
    {
        [SerializeField] private Storage storage;
        [SerializeField] private StorageItem[] items;

        private void Start()
        {
            _ = UpdateStorage();
        }

        private async UniTaskVoid UpdateStorage()
        {
            UniTask.SwitchToMainThread();
            while (storage != null)
            {
                await TickSystem.WaitForNextTickAsync();
            
            
                storage.ClearAmounts();
                foreach ((_,IDepositable value) in EntityContainer.Depositables)
                {
                    if (value is not Warehouse warehouse) continue;
                
                    if (storage.ItemElements.TryGetValue("Wood", out var wood))
                        wood.amount += warehouse.CurrentInfo.Wood;
                    if (storage.ItemElements.TryGetValue("Slime", out var slime))
                        slime.amount += warehouse.CurrentInfo.Slime;
                    if (storage.ItemElements.TryGetValue("Metal", out var metal))
                        metal.amount += warehouse.CurrentInfo.Metal;
                    if (storage.ItemElements.TryGetValue("Berry", out var berry))
                        berry.amount += warehouse.CurrentInfo.Berry;
                }

                await UpdateUI();
            }
            
        }

        public void DecreasingTexts(Cost costs)
        {
            foreach (var storageItem in items)
            {
                switch (storageItem.name)
                {
                    case "Wood":
                        storageItem.DecreasingText(costs.wood.ToString());
                        break;
                    case "Slime":
                        storageItem.DecreasingText(costs.slime.ToString());
                        break;
                    case "Metal":
                        storageItem.DecreasingText(costs.metal.ToString());
                        break;
                    case "Berry":
                        storageItem.DecreasingText(costs.berry.ToString());
                        break;
                }
            }
        }
        public void DecreasingTextsDisable()
        {
            foreach (var storageItem in items)
            {
                storageItem.DecreasingTextDisable();
            }
        }

        private async UniTask UpdateUI()
        {
            foreach (var item in items)
            {
                if(!storage.ItemElements.TryGetValue(item.name, out var element)) continue;
                
                item.SetText(element.amount.ToString());
                item.SetIcon(element.icon);
                // item.gameObject.SetActive(element.amount > 0);
            }   
            await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);
        }
    }
}