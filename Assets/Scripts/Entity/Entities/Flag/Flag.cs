using System;
using Engine;
using Entity.Entities.Worker.Actions;
using Structure;
using UnityEngine;

namespace Entity.Entities.Flag
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Flag : MonoBehaviour , IConvertable, IPurchasable
    {
        [SerializeField] private Sprite convertedSprite;
        [SerializeField] private Sprite unconvertedSprite;
        private FlagConverting _flagConverting;
        private SpriteRenderer _spriteRenderer;

        public IPurchasable.Cost PurchaseCost { get; set; } = new(4, 4, 4, 4);
        public bool IsConverted { get; set; } = false;

        public TickActionBehaviour ConvertingBehaviour() => _flagConverting;
        public void Initialize(IPurchasable.Cost workerMaterials)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _flagConverting = GetComponent<FlagConverting>();
            if (_flagConverting != null)
                _flagConverting.Initialize(this, workerMaterials);
        }
        
        public void Convert(IPurchasable.Cost givenMaterials)
        {
            if (IsConverted) return;
            
            IPurchasable.Cost requiredCost = PurchaseCost;

            requiredCost.Metal -= givenMaterials.Metal;
            requiredCost.Wood -= givenMaterials.Wood;
            requiredCost.Slime -= givenMaterials.Slime;
            requiredCost.Berry -= givenMaterials.Berry;

            if (requiredCost.TotalCost > 0)
                return;
            
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