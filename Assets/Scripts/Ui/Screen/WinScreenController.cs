using BoardGame.Utility;
using Supyrb;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoardGame.Ui.Screen
{
    public class WinScreenController : UiMenu
    {
        [SerializeField] private Button _replayButton;
        [SerializeField] private TMP_Text _winnerText;

        private void OnEnable()
        {
            Signals.Get<GameSignals.GameWinner>()
                .AddListener(OnGameWinnerChosen);
            _replayButton.onClick.AddListener(OnReplayButtonClicked);
        }

        private void OnDisable()
        {
            Signals.Get<GameSignals.GameWinner>()
                .RemoveListener(OnGameWinnerChosen);
            _replayButton.onClick.RemoveListener(OnReplayButtonClicked);
        }

        private void OnReplayButtonClicked()
        {
            Signals.Get<GameSignals.RestartGame>()
                .Dispatch();
            Signals.Get<GameSignals.ShowMenu>().Dispatch(MenuType.GameRound);
        }

        private void OnGameWinnerChosen(string winner)
        {
            _winnerText.text = winner + " Won!!!";
        }
    }
}