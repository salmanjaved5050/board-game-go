using BoardGame.Utility;
using UnityEngine;

namespace BoardGame.Core
{
    /// <summary>
    /// Contains the game board state of all the slots on game board and is managed by game logic
    /// </summary>
    internal class GameBoardState
    {
        private Vector2Int _boardSize;

        internal GameBoardState(Vector2Int boardSize)
        {
            _boardSize = boardSize;
            InitializeBoardState();
        }

        private BoardSlotState[,] _boardSlotStates;

        private void InitializeBoardState()
        {
            _boardSlotStates = new BoardSlotState[_boardSize.x, _boardSize.y];
            SetStatesOwnerToNone();
        }

        private void SetStatesOwnerToNone()
        {
            for (int x = 0; x < _boardSize.x; x++)
            {
                for (int z = 0; z < _boardSize.y; z++)
                {
                    _boardSlotStates[x, z] = new BoardSlotState(BoardSlotOwner.None);
                }
            }
        }

        internal bool CheckIfSlotFree(SlotLocation location)
        {
            bool isLocationFree = _boardSlotStates[location.x, location.y]
                .GetOwner() == BoardSlotOwner.None;
            return isLocationFree;
        }

        internal BoardSlotOwner GetOwnerAtSlot(SlotLocation location)
        {
            return _boardSlotStates[location.x, location.y]
                .GetOwner();
        }

        internal void SetOwnerAtSlot(SlotLocation location, BoardSlotOwner owner)
        {
            _boardSlotStates[location.x, location.y]
                .SetOwner(owner);
        }

        internal BoardSlotState[,] GetBoardState()
        {
            return _boardSlotStates;
        }

        internal void ResetGameState()
        {
            SetStatesOwnerToNone();
        }
    }

    /// <summary>
    /// State of an individual slot on the game board
    /// </summary>
    internal class BoardSlotState
    {
        private BoardSlotOwner _slotOwner;

        internal BoardSlotState(BoardSlotOwner slotOwner)
        {
            _slotOwner = BoardSlotOwner.None;
        }

        internal BoardSlotOwner GetOwner() => _slotOwner;

        internal void SetOwner(BoardSlotOwner slotOwner)
        {
            _slotOwner = slotOwner;
        }
    }

    internal enum BoardSlotOwner
    {
        None,
        Player1,
        Player2
    }
}