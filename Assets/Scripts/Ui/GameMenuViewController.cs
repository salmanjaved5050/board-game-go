using Supyrb;
using TMPro;
using UnityEngine;
using Utility;
using UnityEngine.UI;

namespace BoardGame.Ui
{
    public class GameMenuViewController : MonoBehaviour
    {
        [SerializeField] private GameObject _gameInfoPanel;
        [SerializeField] private TMP_Text _playerTurnText;
        [SerializeField] private TMP_Text _player1ScoreText;
        [SerializeField] private TMP_Text _player2ScoreText;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _replayButton;
        [SerializeField] private TMP_Text _winnerText;

        private void OnEnable()
        {
            Signals.Get<GameSignals.PlayerTurnChanged>()
                .AddListener(OnPlayerTurnChagned);
            Signals.Get<GameSignals.PlayerScoreChanged>()
                .AddListener(OnPlayerScoreChanged);
            Signals.Get<GameSignals.GameFinished>()
                .AddListener(OnGameFinished);
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _replayButton.onClick.AddListener(OnReplayButtonClicked);
        }

        private void OnDisable()
        {
            Signals.Get<GameSignals.PlayerTurnChanged>()
                .RemoveListener(OnPlayerTurnChagned);
            Signals.Get<GameSignals.PlayerScoreChanged>()
                .RemoveListener(OnPlayerScoreChanged);
            Signals.Get<GameSignals.GameFinished>()
                .RemoveListener(OnGameFinished);
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _replayButton.onClick.RemoveListener(OnReplayButtonClicked);
        }

        private void ResetGameInfo()
        {
            _playerTurnText.text = "Player 1";
            _player1ScoreText.text = "Player 1 : 0";
            _player2ScoreText.text = "Player 2 : 0";
        }

        private void OnPlayerTurnChagned(PlayerTurn playerTurn)
        {
            _playerTurnText.text = playerTurn.ToString();
        }

        private void OnPlayerScoreChanged(PlayerTurn playerTurn, int score)
        {
            if (playerTurn == PlayerTurn.Player1)
            {
                _player1ScoreText.text = "Player 1 : " + score;
            }
            else
            {
                _player2ScoreText.text = "Player 2 : " + score;
            }
        }

        private void OnPlayButtonClicked()
        {
            Signals.Get<GameSignals.StartGame>()
                .Dispatch();
            _gameInfoPanel.SetActive(true);
            _playButton.gameObject.SetActive(false);
        }
        
        private void OnReplayButtonClicked()
        {
            ResetGameInfo();
            _gameInfoPanel.SetActive(true);
            _replayButton.gameObject.SetActive(false);
            _winnerText.gameObject.SetActive(false);
            Signals.Get<GameSignals.RestartGame>()
                .Dispatch();
        }

        private void OnGameFinished(string winner)
        {
            _replayButton.gameObject.SetActive(true);
            _winnerText.text = winner + " Won!!!";
            _winnerText.gameObject.SetActive(true);
            _gameInfoPanel.SetActive(false);
        }
    }
}