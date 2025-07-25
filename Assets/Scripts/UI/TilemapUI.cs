using System;
using Map.Tiles;
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
        [SerializeField]
        private Camera mainCamera;
        [SerializeField]
        private Tilemap baseTilemap;
        [SerializeField]
        private Tilemap overlayTilemap;
        [FormerlySerializedAs("buildingPanel")] [SerializeField]
        private BuyingPanel buyingPanel;

        private void Update()
        {
            if (buyingPanel.isActive && (UnityEngine.Input.GetMouseButtonDown(0) || UnityEngine.Input.GetMouseButtonDown(1)))
            {
                // Hide the building panel when clicking on the tilemap
                if (!EventSystem.current.IsPointerOverGameObject())
                    buyingPanel.Hide();
            }
            
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                Vector3 worldPos = mainCamera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                worldPos.z = 0;
                Vector2Int gridPos = GridUtilities.WorldPositionToGridPosition(worldPos);
                Vector3 spawnPos = GridUtilities.GridPositionToWorldPosition(gridPos);
                
                var tile = overlayTilemap.GetTile(new Vector3Int(gridPos.x, gridPos.y, 0));

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
                        new ButtonActionElement(BuyableActions.BuildingActions[StructureTileType.Warehouse], spawnPos.x,
                            spawnPos.y);
                }
                buyingPanel.Initialize(worldPos, buildingActions);
                buyingPanel.Show();

            }
        }
    }
}