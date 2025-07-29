using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonHoverScale : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
    {
        public float targetScaleMultiplier = 1.1f;
        public float animationSpeed = 10f;

        private Vector3 originalScale;
        private bool isHovered = false;
        
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        void Start()
        {
            originalScale = transform.localScale;
        }

        void Update()
        {
            if (!button.interactable) return;
            Vector3 desiredScale = isHovered ? (targetScaleMultiplier * originalScale) : originalScale;
            transform.localScale = Vector3.Lerp(transform.localScale, desiredScale, Time.deltaTime * animationSpeed);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isHovered = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isHovered = false;
        }
    }
}