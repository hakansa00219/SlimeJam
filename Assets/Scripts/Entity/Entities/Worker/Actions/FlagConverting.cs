using Engine;
using Entity.Entities.Flag;
using UnityEngine;

namespace Entity.Entities.Worker.Actions
{
    public class FlagConverting : TickActionBehaviour
    {
        protected override int TickDelay { get; set; } = 20;

        private IConvertable _convertable;
        private GameObject _endPanel;
        public void Initialize(IConvertable convertable, GameObject endPanel)
        {
            _convertable = convertable;
            _endPanel = endPanel;
        }
        protected override void OnTick()
        {
            _convertable.Convert();

            if (EntityContainer.CheckWinCondition())
            {
                Debug.Log("All flags converted! You win!");
                // Pause the game
                _endPanel.SetActive(true);
            }
        }
    }
}