using Entity;
using Entity.Entities;
using Entity.Entities.Berry;
using Entity.Entities.Metal;
using Entity.Entities.Slime;
using Entity.Entities.Worker.Actions;
using UnityEngine;
using Tree = Entity.Entities.Tree.Tree;

namespace Structure
{
    public class Gym : MonoBehaviour , IUpgrader, IStructure
    {
        private Entity.Entities.Worker.Worker _worker;
        private WorkerMovement _movement;

        private void Start()
        {
            _worker = EntityContainer.Worker;
            
            if (_worker == null)
            {
                Debug.LogError("No Worker assigned to Gym");
                return;
            }
            
            _movement = _worker.GetComponent<WorkerMovement>();

            
        }

        public void UpgradeWorkerStorage()
        {
            _worker.Capacity = new IStorage.StorageCapacity()
            {
                Berry = _worker.Capacity.Berry * 2,
                Metal = _worker.Capacity.Metal * 2,
                Slime = _worker.Capacity.Slime * 2,
                Wood = _worker.Capacity.Wood * 2,
                Total = _worker.Capacity.Total * 2
            };
        }

        public void UpgradeMovementspeed()
        {
            //Increase movement tick speed
            _movement.UpgradeTickDelay(1);
        }

        public void UpgradeBerryGatherSpeed()
        {
            //Increase berry gather tick speed
            foreach (var (_, value) in EntityContainer.Gatherables)
            {
                if (value is Berry berry)
                    berry.GatheringBehaviour().UpgradeTickDelay(2);
            }
        }

        public void UpgradeMetalGatherSpeed()
        {
            //Increase metal gather tick speed
            foreach (var (_, value) in EntityContainer.Gatherables)
            {
                if (value is Metal metal)
                    metal.GatheringBehaviour().UpgradeTickDelay(2);
            }
        }

        public void UpgradeWoodGatherSpeed()
        {
            //Increase wood gather tick speed
            foreach (var (_, value) in EntityContainer.Gatherables)
            {
                if (value is Tree tree)
                    tree.GatheringBehaviour().UpgradeTickDelay(2);
            }
        }

        public void UpgradeSlimeGatherSpeed()
        {
            //Increase slime gather tick speed
            foreach (var (_, value) in EntityContainer.Gatherables)
            {
                if (value is Slime slime)
                    slime.GatheringBehaviour().UpgradeTickDelay(2);
            }
        }
    }
}