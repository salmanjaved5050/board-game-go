using System.Collections;
using System.Collections.Generic;
using BoardGame.Core;
using BoardGame.Utility;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;

namespace GameBoard.UnitTests.PlayModeTests
{
    /// <summary>
    /// This class checks for 4 traps created by player 1 and captures player 2
    /// enemy stones placed within. The locations of stones are hard coded for
    /// testing prupose in order to check if traps logic is working correctly.
    /// </summary>
    public class GameBoardTrapTest
    {
        [UnityTest]
        public IEnumerator GameBoardTrapTestWithEnumeratorPasses()
        {
            Vector2Int boardSize = new Vector2Int(12, 12);
 
            GameBoardUnitTestHelper gameBoardHelper = new GameBoardUnitTestHelper(boardSize);

            List<SlotLocation> player1Locations = new List<SlotLocation>()
            {
                new SlotLocation(7, 6),
                new SlotLocation(6, 5),
                new SlotLocation(5, 6),
                new SlotLocation(7, 8),
                new SlotLocation(6, 9),
                new SlotLocation(5, 8),
                new SlotLocation(8, 7),
                new SlotLocation(4, 7),
            };

            List<SlotLocation> player2Locations = new List<SlotLocation>()
            {
                new SlotLocation(6, 6),
                new SlotLocation(6, 8),
                new SlotLocation(7, 7),
                new SlotLocation(5, 7)
            };

            gameBoardHelper.PlacePlayer1StonesFormingTrapAtLocations(player1Locations);
            gameBoardHelper.PlacePlayer2StonesToBeCapturedAtLocations(player2Locations);
            gameBoardHelper.PlacePlayer1StoneCompletingTrapAtLocation(new SlotLocation(6, 7));

            Assert.IsTrue(gameBoardHelper.CheckIfPlayer2StonesCaptured(player2Locations));

            yield return null;
        }
    }
}