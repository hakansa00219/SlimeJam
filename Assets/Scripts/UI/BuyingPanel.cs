using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BuyingPanel : MenuViewer
    {
        [SerializeField]
        private float radius = 100f;
        [SerializeField]
        private RectTransform buttonPrefab;

        [SerializeField] private GameObject tooltipPanel;
        [SerializeField] private StorageUI storageUI;
        public override void Initialize(Vector2 worldPosition, params ButtonActionElement[] buildings)
        {
            Panel = GetComponent<RectTransform>();
            
            Design(buildings);
            SetPanelPosition(worldPosition);
        }

        public void Design(params ButtonActionElement[] buildings)
        {
            //Designing logic for panel.
            //Cleaning children
            foreach (Transform child in Panel.transform)
                Destroy(child.gameObject);
            
            int buildingCount = buildings.Length;
            
            if (buildingCount == 0) 
            {
                Debug.LogWarning("No buildings provided for design.");
                return;
            }

            for (int i = 0; i < buildingCount; i++)
            {
                float angle = i * (360f / buildingCount);
                float radians = angle * Mathf.Deg2Rad;
                float x = Mathf.Cos(radians) * radius;
                float y = Mathf.Sin(radians) * radius;
                
                RectTransform buildingButton = Instantiate(buttonPrefab, Panel.transform);

                CircleButton circleButton = buildingButton.GetComponent<CircleButton>();
                if(circleButton != null)
                    circleButton.Initialize(buildings[i].ButtonIcon);
                TooltipHandler tooltipHandler = circleButton.GetComponent<TooltipHandler>();
                if (tooltipHandler != null)
                {
                    tooltipHandler.Initialize(tooltipPanel, storageUI, buildings[i].Description, buildings[i].Cost,
                        buildings[i].WorldPositionX, buildings[i].WorldPositionY);
                }
                    
                
                if(buildingButton != null)
                    buildingButton.anchoredPosition = new Vector2(x, y);
                
                Button btn = buildingButton.GetComponent<Button>();

                btn.interactable = buildings[i].IsAffordable;
                
                var index = i;
                btn.onClick.AddListener(() =>
                {
                    buildings[index].OnClickAction(buildings[index].WorldPositionX, buildings[index].WorldPositionY);
                    tooltipPanel.gameObject.SetActive(false);
                    storageUI.DecreasingTextsDisable();
                    Hide();
                });

            }
            
            
        }
    }
}