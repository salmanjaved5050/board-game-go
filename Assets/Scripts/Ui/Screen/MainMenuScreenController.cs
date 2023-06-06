using BoardGame.Utility;
using Supyrb;
using UnityEngine;
using UnityEngine.UI;

namespace BoardGame.Ui.Screen
{
    public class MainMenuScreenController : UiMenu
    {
        [SerializeField] private Button _playButton;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            Signals.Get<GameSignals.StartGame>()
                .Dispatch();
            
            Signals.Get<GameSignals.ShowMenu>().Dispatch(MenuType.GameRound);

            _playButton.gameObject.SetActive(false);
        }
    }
}