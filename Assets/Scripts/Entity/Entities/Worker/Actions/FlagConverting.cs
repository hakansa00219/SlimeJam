using Engine;
using Entity.Entities.Flag;
using Scene;
using UnityEngine;

namespace Entity.Entities.Worker.Actions
{
    public class FlagConverting : TickActionBehaviour
    {
        protected override int TickDelay { get; set; } = 20;

        private IConvertable _convertable;
        private GameObject _endPanel;
        public void Initialize(IConvertable convertable)
        {
            _convertable = convertable;
            _endPanel = Ending.Instance.EndPanel;

            if (_endPanel == null)
            {
                Debug.LogError("No end panel");
            }
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