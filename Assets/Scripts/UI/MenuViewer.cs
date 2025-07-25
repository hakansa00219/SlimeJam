using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class MenuViewer : MonoBehaviour
    {
        protected RectTransform Panel;
        [FormerlySerializedAs("IsActive")] public bool isActive = false;

        public abstract void Initialize(Vector2 worldPosition, params ButtonActionElement[] buildings);
        protected virtual void Awake()
        {
            Panel = GetComponent<RectTransform>();
        }

        protected virtual void SetPanelPosition(Vector2 worldPosition)
        {
            if (Panel == null) return;
            
            Panel.localPosition = worldPosition;
        }

        public virtual void Show()
        {
            Panel.gameObject.SetActive(true);
            isActive = true;
        }

        public virtual void Hide()
        {
            Panel.gameObject.SetActive(false);
            isActive = false;
        }

        public virtual void Destroy()
        {
            Panel.gameObject.SetActive(false);
            isActive = false;
            Destroy(Panel.gameObject);
        }
        
    }
}