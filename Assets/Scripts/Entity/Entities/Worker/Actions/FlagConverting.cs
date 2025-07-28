using Engine;
using Entity.Entities.Flag;
using Structure;
using UnityEngine;

namespace Entity.Entities.Worker.Actions
{
    public class FlagConverting : TickActionBehaviour
    {
        protected override int TickDelay { get; } = 20;

        private IConvertable _convertable;
        private IPurchasable.Cost _givenMaterials;
        private bool _isConfirmed = false;
        public void Initialize(IConvertable convertable, IPurchasable.Cost givenMaterials)
        {
            _convertable = convertable;
            _givenMaterials = givenMaterials;
        }
        protected override void OnTick()
        {
            if (!_isConfirmed)
            {
                _isConfirmed = true;
                return;
            }
            
            _convertable.Convert(_givenMaterials);

            if (EntityContainer.CheckWinCondition())
            {
                Debug.Log("All flags converted! You win!");
                // Pause the game
                Time.timeScale = 0f;
            }
        }
    }
}