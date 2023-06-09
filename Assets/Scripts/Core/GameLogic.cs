using System.Collections.Generic;
using BoardGame.Utility;
using Supyrb;
using UnityEngine;

namespace BoardGame.Core
{
    /// <summary>
    /// Controls the main game logic and state of the game board
    /// </summary>
    internal class GameLogic
    {
        private GameBoardState _gameBoardState;
        private PlayerTurn _playerTurn;
        private int _player1Score;
        private int _player2Score;
        private Vector2Int _boardSize;

        internal GameLogic(Vector2Int boardSize)
        {
            _boardSize = boardSize;
            InitializeGameLogic(boardSize);
        }

        private void InitializeGameLogic(Vector2Int boardSize)
        {
            _playerTurn = PlayerTurn.Player1;
            _gameBoardState = new GameBoardState(boardSize);
        }

        private bool IsSlotLocationOnBoard(SlotLocation slotLocation)
        {
            return (slotLocation.x >= 0 && slotLocation.y >= 0 && slotLocation.x < _boardSize.x && slotLocation.y < _boardSize.y);
        }

        private List<SlotLocation> GetOffsetSlotsAroundLocation(SlotLocation slotLocation, List<SlotLocation> offsets)
        {
            List<SlotLocation> neighbours = new List<SlotLocation>();

            for (int i = 0; i < offsets.Count; i++)
            {
                SlotLocation neighbour = new SlotLocation(slotLocation.x + offsets[i]
                    .x, slotLocation.y + offsets[i]
                    .y);

                if (IsSlotLocationOnBoard(neighbour))
                {
                    neighbours.Add(neighbour);
                }
            }

            return neighbours;
        }

        private bool CheckIfSlotsAreOccupiedByOwner(List<SlotLocation> slotLocations, BoardSlotOwner owner)
        {
            int locationsCount = slotLocations.Count;
            for (int i = 0; i < locationsCount; i++)
            {
                BoardSlotOwner slotOwner = _gameBoardState.GetOwnerAtSlot(slotLocations[i]);
                if (slotOwner != owner)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckIfTrapIsFormedByOwner(List<SlotLocation> trapSlots, BoardSlotOwner owner)
        {
            return CheckIfSlotsAreOccupiedByOwner(trapSlots, owner);
        }

        private bool CheckIfTrapCapturesEnemyStone(SlotTrap trap, BoardSlotOwner currentSlotOwner, BoardSlotOwner targetOwner)
        {
            bool isStoneCaptureable = false;
            if (CheckIfTrapIsFormedByOwner(trap.GetTrapBoundries(), currentSlotOwner))
            {
                SlotLocation targetLocation = trap.GetTargetLocation();
                if (IsSlotLocationOnBoard(targetLocation))
                {
                    if (_gameBoardState.GetOwnerAtSlot(targetLocation) == targetOwner)
                    {
                        isStoneCaptureable = true;
                    }
                }
            }

            return isStoneCaptureable;
        }

        private void CheckForTraps(SlotLocation location)
        {
            BoardSlotOwner currentSlotOwner = _playerTurn == PlayerTurn.Player1 ? BoardSlotOwner.Player1 : BoardSlotOwner.Player2;
            BoardSlotOwner targetOwner = _playerTurn == PlayerTurn.Player1 ? BoardSlotOwner.Player2 : BoardSlotOwner.Player1;

            SlotTrap leftTrap = new SlotTrap(location, _boardSize, GameConstants.BoardGameOffsets.LeftTrapOffsets,
                GameConstants.BoardGameOffsets.LeftOrthognalSlotOffset);
            if (CheckIfTrapCapturesEnemyStone(leftTrap, currentSlotOwner, targetOwner))
            {
                _gameBoardState.SetOwnerAtSlot(leftTrap.GetTargetLocation(), BoardSlotOwner.None);
                PlayerCapturedStone();
            }

            SlotTrap rightTrap = new SlotTrap(location, _boardSize, GameConstants.BoardGameOffsets.RightTrapOffsets,
                GameConstants.BoardGameOffsets.RightOrthognalSlotOffset);
            if (CheckIfTrapCapturesEnemyStone(rightTrap, currentSlotOwner, targetOwner))
            {
                _gameBoardState.SetOwnerAtSlot(rightTrap.GetTargetLocation(), BoardSlotOwner.None);
                PlayerCapturedStone();
            }

            SlotTrap topTrap = new SlotTrap(location, _boardSize, GameConstants.BoardGameOffsets.TopTrapOffsets,
                GameConstants.BoardGameOffsets.TopOrthognalSlotOffset);
            if (CheckIfTrapCapturesEnemyStone(topTrap, currentSlotOwner, targetOwner))
            {
                _gameBoardState.SetOwnerAtSlot(topTrap.GetTargetLocation(), BoardSlotOwner.None);
                PlayerCapturedStone();
            }

            SlotTrap bottomTrap = new SlotTrap(location, _boardSize, GameConstants.BoardGameOffsets.BottomTrapOffsets,
                GameConstants.BoardGameOffsets.BottomOrthognalSlotOffset);
            if (CheckIfTrapCapturesEnemyStone(bottomTrap, currentSlotOwner, targetOwner))
            {
                _gameBoardState.SetOwnerAtSlot(bottomTrap.GetTargetLocation(), BoardSlotOwner.None);
                PlayerCapturedStone();
            }
        }

        private void EvaluateMoveAtLocationAndUpdateGameState(SlotLocation location)
        {
            CheckForTraps(location);
        }

        private void PlayerCapturedStone()
        {
            int score;
            if (_playerTurn == PlayerTurn.Player1)
            {
                score = ++_player1Score;
            }
            else
            {
                score = ++_player2Score;
            }

            Signals.Get<GameSignals.PlayerScoreChanged>()
                .Dispatch(_playerTurn, score);
        }

        private void ChangePlayerTurn()
        {
            _playerTurn = _playerTurn == PlayerTurn.Player1 ? PlayerTurn.Player2 : PlayerTurn.Player1;
        }
        
        internal PlayerTurn GetCurrentPlayerTurn()
        {
            return _playerTurn;
        }

        internal bool IsGameFinished()
        {
            return _player1Score >= 3 || _player2Score >= 3;
        }

        internal string GetWinner(bool isGameForfeitByPlayer = false)
        {
            string winner;
            if (isGameForfeitByPlayer)
            {
                winner = _playerTurn == PlayerTurn.Player1 ? "Player 2" : "Player 1";
            }
            else
            {
                winner = _player1Score > _player2Score ? "Player 1" : "Player 2";
            }

            return winner;
        }

        internal bool IsMoveValidAtLocation(SlotLocation location)
        {
            bool isSlotFree = _gameBoardState.CheckIfSlotFree(location);
            if (isSlotFree)
            {
                BoardSlotOwner owner = _playerTurn == PlayerTurn.Player1 ? BoardSlotOwner.Player1 : BoardSlotOwner.Player2;
                _gameBoardState.SetOwnerAtSlot(location, owner);

                EvaluateMoveAtLocationAndUpdateGameState(location);
                ChangePlayerTurn();
            }

            return isSlotFree;
        }

        internal void ResetGameLogic()
        {
            _playerTurn = PlayerTurn.Player1;
            _player1Score = 0;
            _player2Score = 0;
            _gameBoardState.ResetGameState();
        }

        internal BoardSlotState[,] GetBoardState()
        {
            return _gameBoardState.GetBoardState();
        }

        #region Helper Methods For Unit Testing Prupose

        internal void SetOwnersAtLocations(List<SlotLocation> locations, BoardSlotOwner slotOwner)
        {
            for (int i = 0; i < locations.Count; i++)
            {
                SetOwnerAtLocationAndCheckForCaptures(locations[i], slotOwner);
            }
        }

        internal void SetOwnerAtLocationAndCheckForCaptures(SlotLocation location, BoardSlotOwner slotOwner)
        {
            _gameBoardState.SetOwnerAtSlot(location, slotOwner);
            CheckForTraps(location);
        }

        internal bool CheckIfPlayer2LocationsAreEmpty(List<SlotLocation> locations, BoardSlotOwner boardSlotOwner)
        {
            return CheckIfSlotsAreOccupiedByOwner(locations, BoardSlotOwner.None);
        }

        #endregion
    }
}