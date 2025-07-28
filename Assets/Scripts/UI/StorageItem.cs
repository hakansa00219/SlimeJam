using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class StorageItem : MonoBehaviour
    {
        [SerializeField] private Text textComponent;
        [SerializeField] private Image image;

        public void SetIcon(Sprite icon)
        {
            image.sprite = icon;
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