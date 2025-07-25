using System;

namespace UI
{
    public class ButtonActionElement
    {
        public Action<float, float> OnClickAction;
        public float WorldPositionX;
        public float WorldPositionY;
        
        public ButtonActionElement(Action<float, float> onClickAction, float worldPositionX, float worldPositionY)
        {
            OnClickAction = onClickAction;
            WorldPositionX = worldPositionX;
            WorldPositionY = worldPositionY;
        }
    }
}