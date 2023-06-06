using System.Collections.Generic;
using BoardGame.Utility;
using UnityEngine;

namespace BoardGame.Core
{
    public class GameBoardUnitTestHelper
    {
        private readonly GameLogic _gameLogic;

        public GameBoardUnitTestHelper(Vector2Int boardSize)
        {
            _gameLogic = new GameLogic(boardSize);
        }

        public void PlacePlayer1StonesFormingTrapAtLocations(List<SlotLocation> locations)
        {
            _gameLogic.SetOwnersAtLocations(locations, BoardSlotOwner.Player1);
        }

        public void PlacePlayer2StonesToBeCapturedAtLocations(List<SlotLocation> locations)
        {
            _gameLogic.SetOwnersAtLocations(locations, BoardSlotOwner.Player2);
        }

        public void PlacePlayer1StoneCompletingTrapAtLocation(SlotLocation location)
        {
            _gameLogic.SetOwnerAtLocationAndCheckForCaptures(location, BoardSlotOwner.Player1);
        }

        public bool CheckIfPlayer2StonesCaptured(List<SlotLocation> locations)
        {
            return _gameLogic.CheckIfPlayer2LocationsAreEmpty(locations, BoardSlotOwner.None);
        }
    }
}