using Data;
using Engine;
using Entity.Entities.Worker.Actions;
using Scriptable;
using UnityEngine;

namespace Entity.Entities.Flag
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(FlagConverting))]
    [RequireComponent(typeof(Transferring))]
    public class Flag : MonoBehaviour , IConvertable, IPurchasable
    {
        [SerializeField] private Sprite convertedSprite;
        [SerializeField] private Sprite unconvertedSprite;
        [SerializeField] private Costs buildings;
        private FlagConverting _flagConverting;
        private Transferring _transferring;
        private SpriteRenderer _spriteRenderer;
        
        public Cost PurchaseCost { get; set; }
        public bool IsPurchased { get; set; } = false;
        public bool IsTransferred { get; set; } = false;
        public bool IsConverted { get; set; } = false;

        public TickActionBehaviour ConvertingBehaviour() => _flagConverting;
        public TickActionBehaviour TransferringBehaviour() => _transferring;

        public void Initialize(IStorage workerStorage)
        {
            PurchaseCost = buildings.Clone("Flag");
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _flagConverting = GetComponent<FlagConverting>();
            _transferring = GetComponent<Transferring>();
            if (_flagConverting != null)
                _flagConverting.Initialize(this);
            if (_transferring != null)
                _transferring.Initialize(this, workerStorage);
        }
        
        public void Convert()
        {
            if (IsConverted) return;
            if (!IsPurchased) return;
            
            IsConverted = true;
            // Additional logic for converting the flag can be added here
            _spriteRenderer.sprite = convertedSprite;
            Debug.Log("Flag has been converted.");
        }

        public void TransferConditionCheck()
        {
            IsPurchased = PurchaseCost.TotalCost == 0;
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