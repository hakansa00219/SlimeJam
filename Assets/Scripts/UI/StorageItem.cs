using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StorageItem : MonoBehaviour
    {
        [SerializeField] private Text textComponent;
        [SerializeField] private Image image;
        [SerializeField] private Text decreasingText;

        public void SetIcon(Sprite icon)
        {
            image.sprite = icon;
        }

        public void DecreasingText(string text)
        {
            decreasingText.text = $"-{text}";
            decreasingText.gameObject.SetActive(true);
        }

        public void DecreasingTextDisable()
        {
            decreasingText.gameObject.SetActive(false);
        }
        
        public void SetText(string text)
        {
            if (textComponent != null)
            {
                textComponent.text = text;
            }
            else
            {
                Debug.LogWarning("Text component is not assigned in StorageItem.");
            }
        }
    }
    
}