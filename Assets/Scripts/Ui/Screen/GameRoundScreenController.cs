using BoardGame.Utility;
using Supyrb;
using TMPro;
using UnityEngine;

namespace BoardGame.Ui.Screen
{
    public class GameRoundScreenController : UiMenu
    {
        [SerializeField] private TMP_Text _playerTurnText;
        [SerializeField] private TMP_Text _player1ScoreText;
        [SerializeField] private TMP_Text _player2ScoreText;
        
        private void OnEnable()
        {
            Signals.Get<GameSignals.PlayerTurnChanged>()
                .AddListener(OnPlayerTurnChagned);
            Signals.Get<GameSignals.PlayerScoreChanged>()
                .AddListener(OnPlayerScoreChanged);
        }

        private void OnDisable()
        {
            Signals.Get<GameSignals.PlayerTurnChanged>()
                .RemoveListener(OnPlayerTurnChagned);
            Signals.Get<GameSignals.PlayerScoreChanged>()
                .RemoveListener(OnPlayerScoreChanged);
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

        private void ResetPlayerScoresAndTurn()
        {
            _playerTurnText.text = "Player1";
            _player1ScoreText.text = "Player 1 : 0";
            _player2ScoreText.text = "Player 2 : 0";
        }

        public override void ResetMenu()
        {
            ResetPlayerScoresAndTurn();
        }
    }
}
