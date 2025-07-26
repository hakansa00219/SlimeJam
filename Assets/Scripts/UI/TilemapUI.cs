using System;
using Map.Tiles;
using Scriptable;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using Utility;
using Random = UnityEngine.Random;

namespace UI
{
    public class TilemapUI : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Tilemap baseTilemap;
        [SerializeField] private Tilemap overlayTilemap;
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
                
                var tile = overlayTilemap.GetTile(new Vector3Int(gridPos.x, gridPos.y, 0));
                
                outline.SetActive(true);
                outline.transform.position = spawnPos;

                if (tile == null)
                {
                    Debug.LogError("No tile found at position: " + gridPos);
                    return;
                }

                if (tile.name == "Rock")
                {
                    Debug.LogError("Cannot built on rock tile at position: " + gridPos);
                    return;
                }
                
                //Create array and add buildings if condition is met
                int testNumber = Random.Range(1, 12);
                ButtonActionElement[] buildingActions = new ButtonActionElement[testNumber];
                for (int i = 0; i < testNumber; i++)
                {
                    buildingActions[i] =
                        new ButtonActionElement()
                        {
                            OnClickAction = BuyableActions.BuildingActions[StructureTileType.Warehouse],
                            ButtonIcon = tileTextures.structureTiles[StructureTileType.Warehouse],
                            WorldPositionX = spawnPos.x,
                            WorldPositionY = spawnPos.y
                        };
                }
                buyingPanel.Initialize(new Vector2(spawnPos.x, spawnPos.y + 0.5f), buildingActions);
                buyingPanel.Show();

            }
        }
    }
}