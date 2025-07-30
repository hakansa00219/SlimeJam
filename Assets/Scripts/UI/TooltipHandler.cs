using System;
using Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace UI
{
    public class TooltipHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private GameObject _tooltipPanel;
        public float delay = 1f;

        private bool _isPointerOver = false;
        private float _timer = 0f;
        private int _gridX, _gridY;
        private string _tooltipText;
        private StorageUI _storageUI;
        private Cost _cost;

        public void Initialize(GameObject tooltipPanel, StorageUI storageUI, string infoText, Cost cost, float x, float y)
        {
            _tooltipText = infoText;
            _tooltipPanel = tooltipPanel;
            _storageUI = storageUI;
            _cost = cost;
            _tooltipPanel.transform.position = new Vector2(x + 1f, y + 1f);
        }

        void Update()
        {
            if (_isPointerOver)
            {
                _timer += Time.deltaTime;
                if (_timer >= delay && !_tooltipPanel.activeSelf)
                {
                    _tooltipPanel.SetActive(true);
                }
            }
        }
        private void SetText(string text)
        {
            _tooltipPanel.GetComponentInChildren<Text>().text = text;
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            _isPointerOver = true;
            SetText(_tooltipText);
            _storageUI.DecreasingTexts(_cost);
            _timer = 0f;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isPointerOver = false;
            _timer = 0f;
            SetText("exit");
            _storageUI.DecreasingTextsDisable();
            _tooltipPanel.SetActive(false);
        }
    }

}