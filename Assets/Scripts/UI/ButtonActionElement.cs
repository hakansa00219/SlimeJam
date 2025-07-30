using System;
using Data;
using UnityEngine;

namespace UI
{
    public class ButtonActionElement
    {
        public Action<float, float> OnClickAction;
        public Sprite ButtonIcon;
        public float WorldPositionX;
        public float WorldPositionY;
        public string Description;
        public Cost Cost;
        public bool IsAffordable;
    }
}