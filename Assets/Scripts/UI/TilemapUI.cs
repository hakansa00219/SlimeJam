using System.Collections.Generic;
using System.Linq;
using Data;
using Entity.Entities;
using Map.Tiles;
using Scriptable;
using Structure;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Utility;

namespace UI
{
    public class TilemapUI : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Tilemap baseTilemap;
        [FormerlySerializedAs("overlayTilemap")] [SerializeField] private Tilemap elementTilemap;
        [SerializeField] private BuyingPanel buyingPanel;
        [SerializeField] private TileTextures tileTextures;
        [SerializeField] private GameObject outline;
        [SerializeField] private Storage globalStorage;
        [SerializeField] private Costs buildingCosts;
        [SerializeField] private UpgradeSystem upgradeSystem;

        private void Update()
        {
            if (buyingPanel.isActive && (UnityEngine.Input.GetMouseButtonDown(0) || UnityEngine.Input.GetMouseButtonDown(1)))
            {
                // Hide the building panel when clicking on the tilemap
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    buyingPanel.Hide();
                    outline.SetActive(false);
                }
                    
            }
            
            if (!buyingPanel.isActive && UnityEngine.Input.GetMouseButtonDown(0))
            {
                Vector3 worldPos = mainCamera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                worldPos.z = 0;
                Vector2Int gridPos = GridUtilities.WorldPositionToGridPosition(worldPos);
                Vector3 spawnPos = GridUtilities.GridPositionToWorldPosition(gridPos);
                
                outline.SetActive(true);
                outline.transform.position = spawnPos;
                
                List<ButtonActionElement> buildingActions = new List<ButtonActionElement>();
                
                TileChecks(buildingActions, gridPos, spawnPos);
                

                if (buildingActions.Count == 0)
                    return;
                buyingPanel.Initialize(new Vector2(spawnPos.x, spawnPos.y + 0.5f), buildingActions.ToArray());
                buyingPanel.Show();

            }
        }

        private void TileChecks(List<ButtonActionElement> buildingActions, Vector2Int gridPos, Vector3 spawnPos)
        {
            EntityContainer.Depositables.TryGetValue(gridPos, out var depositable);
            EntityContainer.Upgraders.TryGetValue(gridPos, out var upgrader);
            var elementTile = elementTilemap.GetTile(new Vector3Int(gridPos.x, gridPos.y, 0));
            var baseTile = baseTilemap.GetTile(new Vector3Int(gridPos.x, gridPos.y, 0));

            if (depositable is Warehouse)
            {
                buildingActions.Add(new ButtonActionElement()
                {
                    OnClickAction = BuyableActions.RemoveActions["Warehouse"],
                    ButtonIcon = tileTextures.deleteTileSprite,
                    WorldPositionX = spawnPos.x,
                    WorldPositionY = spawnPos.y,
                    Description = "Remove",
                    Cost = new Cost(0,0,0,0),
                    IsAffordable = true,
                });
                
                buildingActions.Add(new ButtonActionElement()
                {
                    OnClickAction = BuyableActions.WarehouseActions["Storage"],
                    ButtonIcon = tileTextures.capacityTileSprite,
                    WorldPositionX = spawnPos.x,
                    WorldPositionY = spawnPos.y,
                    Description = "Capacity Upgrade",
                    Cost = upgradeSystem.WarehouseStorageUpgrade,
                    IsAffordable = globalStorage.CanAfford(upgradeSystem.WarehouseStorageUpgrade),
                });
                return;
            }

            if (upgrader is Gym)
            {
                buildingActions.Add(new ButtonActionElement()
                {
                    OnClickAction = BuyableActions.RemoveActions["Gym"],
                    ButtonIcon = tileTextures.deleteTileSprite,
                    WorldPositionX = spawnPos.x,
                    WorldPositionY = spawnPos.y,
                    Description = "Remove",
                    Cost = new Cost(0,0,0,0),
                    IsAffordable = true,
                });
                buildingActions.Add(new ButtonActionElement()
                {
                    OnClickAction = BuyableActions.GymActions["Storage"],
                    ButtonIcon = tileTextures.capacityTileSprite,
                    WorldPositionX = spawnPos.x,
                    WorldPositionY = spawnPos.y,
                    Description = "Worker Capacity Upgrade",
                    Cost = upgradeSystem.GymWorkerStorageUpgrade,
                    IsAffordable = globalStorage.CanAfford(upgradeSystem.GymWorkerStorageUpgrade),
                });
                buildingActions.Add(new ButtonActionElement()
                {
                    OnClickAction = BuyableActions.GymActions["Movespeed"],
                    ButtonIcon = tileTextures.elementTiles[TileElementType.Road],
                    WorldPositionX = spawnPos.x,
                    WorldPositionY = spawnPos.y,
                    Description = "Worker Movespeed Upgrade",
                    Cost = upgradeSystem.GymMovespeedUpgrade,
                    IsAffordable = globalStorage.CanAfford(upgradeSystem.GymMovespeedUpgrade),
                });
                buildingActions.Add(new ButtonActionElement()
                {
                    OnClickAction = BuyableActions.GymActions["Berry_Gather_Speed"],
                    ButtonIcon = tileTextures.elementTiles[TileElementType.Berry],
                    WorldPositionX = spawnPos.x,
                    WorldPositionY = spawnPos.y,
                    Description = "Berry Gather Speed Upgrade",
                    Cost = upgradeSystem.GymBerryGatherSpeedUpgrade,
                    IsAffordable = globalStorage.CanAfford(upgradeSystem.GymBerryGatherSpeedUpgrade),
                });
                buildingActions.Add(new ButtonActionElement()
                {
                    OnClickAction = BuyableActions.GymActions["Metal_Gather_Speed"],
                    ButtonIcon = tileTextures.elementTiles[TileElementType.Metal],
                    WorldPositionX = spawnPos.x,
                    WorldPositionY = spawnPos.y,
                    Description = "Metal Gather Speed Upgrade",
                    Cost = upgradeSystem.GymMetalGatherSpeedUpgrade,
                    IsAffordable = globalStorage.CanAfford(upgradeSystem.GymMetalGatherSpeedUpgrade),
                });
                buildingActions.Add(new ButtonActionElement()
                {
                    OnClickAction = BuyableActions.GymActions["Slime_Gather_Speed"],
                    ButtonIcon = tileTextures.elementTiles[TileElementType.Slime],
                    WorldPositionX = spawnPos.x,
                    WorldPositionY = spawnPos.y,
                    Description = "Slime Gather Speed Upgrade",
                    Cost = upgradeSystem.GymSlimeGatherSpeedUpgrade,
                    IsAffordable = globalStorage.CanAfford(upgradeSystem.GymSlimeGatherSpeedUpgrade),
                });
                buildingActions.Add(new ButtonActionElement()
                {
                    OnClickAction = BuyableActions.GymActions["Wood_Gather_Speed"],
                    ButtonIcon = tileTextures.elementTiles[TileElementType.Wood],
                    WorldPositionX = spawnPos.x,
                    WorldPositionY = spawnPos.y,
                    Description = "Wood Gather Speed Upgrade",
                    Cost = upgradeSystem.GymWoodGatherSpeedUpgrade,
                    IsAffordable = globalStorage.CanAfford(upgradeSystem.GymWoodGatherSpeedUpgrade),
                });
                return;
            }
            if (elementTile is null || baseTile is null)
            {
                buildingActions.Clear();
                Debug.LogError("No tile found at position: " + gridPos);
                return;
            }

            if (elementTile.name is "Rock" or "Main" or "Flag" || baseTile.name is "Water")
            {
                Debug.LogError("Cannot built on this tile at position: " + gridPos);
                return;
            }

            if (elementTile.name is "Road" or "House" or "Barracks" or "Blacksmith" or "Gym" or "Warehouse")
            {
                buildingActions.Add(new ButtonActionElement()
                {
                    OnClickAction = BuyableActions.RemoveActions[elementTile.name],
                    ButtonIcon = tileTextures.deleteTileSprite,
                    WorldPositionX = spawnPos.x,
                    WorldPositionY = spawnPos.y,
                    Description = "Remove",
                    Cost = new Cost(0,0,0,0),
                    IsAffordable = true,
                });
            }

            if (elementTile.name is "Empty")
            {
                buildingActions.Add(new ButtonActionElement()
                {
                    OnClickAction = BuyableActions.BuildingActions["Road"],
                    ButtonIcon = tileTextures.elementTiles[TileElementType.Road],
                    WorldPositionX = spawnPos.x,
                    WorldPositionY = spawnPos.y,
                    Cost = buildingCosts.Clone("Road"),
                    Description = "Road",
                    IsAffordable = globalStorage.CanAfford(buildingCosts.Clone("Road"))
                });
                buildingActions.Add(new ButtonActionElement()
                {
                    OnClickAction = BuyableActions.BuildingActions["Warehouse"],
                    ButtonIcon = tileTextures.elementTiles[TileElementType.Warehouse],
                    WorldPositionX = spawnPos.x,
                    WorldPositionY = spawnPos.y,
                    Cost = buildingCosts.Clone("Warehouse"),
                    Description = "Warehouse",
                    IsAffordable = globalStorage.CanAfford(buildingCosts.Clone("Warehouse"))
                });
                if (EntityContainer.Upgraders.Count(x => x.Value is Gym) == 0)
                    buildingActions.Add(new ButtonActionElement()
                    {
                        OnClickAction = BuyableActions.BuildingActions["Gym"],
                        ButtonIcon = tileTextures.elementTiles[TileElementType.Gym],
                        WorldPositionX = spawnPos.x,
                        WorldPositionY = spawnPos.y,
                        Cost = buildingCosts.Clone("Gym"),
                        Description = "Gym",
                        IsAffordable = globalStorage.CanAfford(buildingCosts.Clone("Gym"))
                    });
            }
        }
    }
}