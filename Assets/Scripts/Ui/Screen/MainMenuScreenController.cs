using Supyrb;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoardGame.Ui.Screen
{
    public class MainMenuScreenController : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _replayButton;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _replayButton.onClick.AddListener(OnReplayButtonClicked);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _replayButton.onClick.RemoveListener(OnReplayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            Signals.Get<GameSignals.StartGame>()
                .Dispatch();
            _playButton.gameObject.SetActive(false);
        }
        
        private void OnReplayButtonClicked()
        {
            Signals.Get<GameSignals.RestartGame>()
                .Dispatch();
        }
    }
}