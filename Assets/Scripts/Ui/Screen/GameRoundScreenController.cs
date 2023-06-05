using Supyrb;
using TMPro;
using UnityEngine;
using Utility;

namespace BoardGame.Ui.Screen
{
    public class GameRoundScreenController : MonoBehaviour
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
    }
}
