using System;
using System.Collections.Generic;
using Entity.Entities;
using Map.Tiles;
using Scriptable;
using Structure;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Utility;
using Random = UnityEngine.Random;

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
            var elementTile = elementTilemap.GetTile(new Vector3Int(gridPos.x, gridPos.y, 0));
            var baseTile = baseTilemap.GetTile(new Vector3Int(gridPos.x, gridPos.y, 0));

            if (depositable is Warehouse)
            {
                buildingActions.Add(new ButtonActionElement()
                {
                    OnClickAction = BuyableActions.RemoveActions["Warehouse"],
                    ButtonIcon = tileTextures.deleteTileSprite,
                    WorldPositionX = spawnPos.x,
                    WorldPositionY = spawnPos.y
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
                    WorldPositionY = spawnPos.y
                });
                return;
            }

            if (elementTile.name is "Empty")
            {
                buildingActions.Add(new ButtonActionElement()
                {
                    OnClickAction = BuyableActions.BuildingActions["Road"],
                    ButtonIcon = tileTextures.elementTiles[TileElementType.Road],
                    WorldPositionX = spawnPos.x,
                    WorldPositionY = spawnPos.y
                });
                buildingActions.Add(new ButtonActionElement()
                {
                    OnClickAction = BuyableActions.BuildingActions["Warehouse"],
                    ButtonIcon = tileTextures.elementTiles[TileElementType.Warehouse],
                    WorldPositionX = spawnPos.x,
                    WorldPositionY = spawnPos.y
                });
            }
        }
    }
}