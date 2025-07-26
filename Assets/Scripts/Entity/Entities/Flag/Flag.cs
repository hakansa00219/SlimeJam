using System;
using Engine;
using Entity.Entities.Worker.Actions;
using UnityEngine;

namespace Entity.Entities.Flag
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Flag : MonoBehaviour , IConvertable
    {
        [SerializeField] private Sprite convertedSprite;
        [SerializeField] private Sprite unconvertedSprite;
        private FlagConverting _flagConverting;
        private SpriteRenderer _spriteRenderer;
        
        public bool IsConverted { get; set; } = false;

        public TickActionBehaviour ConvertingBehaviour() => _flagConverting;
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _flagConverting = GetComponent<FlagConverting>();
            _flagConverting.Initialize(this);
        }
        
        public void Convert()
        {
            if (IsConverted) return;
            
            IsConverted = true;
            // Additional logic for converting the flag can be added here
            _spriteRenderer.sprite = convertedSprite;
            Debug.Log("Flag has been converted.");
        }
        
        public void Unconvert()
        {
            if (!IsConverted) return;
            
            IsConverted = false;
            // Additional logic for un-converting the flag can be added here
            _spriteRenderer.sprite = unconvertedSprite;
            Debug.Log("Flag has been unconverted.");
        }
    }
}