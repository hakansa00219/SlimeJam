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
        public void Initialize(IConvertable convertable)
        {
            _convertable = convertable;
        }
        protected override void OnTick()
        {
            _convertable.Convert();

            if (EntityContainer.CheckWinCondition())
            {
                Debug.Log("All flags converted! You win!");
                // Pause the game
                Time.timeScale = 0f;
            }
        }
    }
}