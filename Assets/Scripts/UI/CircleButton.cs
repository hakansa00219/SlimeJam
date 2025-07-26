using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CircleButton : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        private Sprite _icon;
        
        public void Initialize(Sprite icon)
        {
            _icon = icon;
            
            iconImage.sprite = _icon;
        }
        
    }
}