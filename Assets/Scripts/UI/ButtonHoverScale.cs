using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ButtonHoverScale : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
    {
        public Vector3 targetScale = new Vector3(1.1f, 1.1f, 1.1f);
        public float animationSpeed = 10f;

        private Vector3 originalScale;
        private bool isHovered = false;

        void Start()
        {
            originalScale = transform.localScale;
        }

        void Update()
        {
            Vector3 desiredScale = isHovered ? targetScale * originalScale : originalScale;
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