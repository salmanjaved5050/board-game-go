using BoardGame.Utility;
using UnityEngine;

namespace BoardGame.Core
{
    internal interface IGameBoardSlot
    {
        void OccupySlot(GameObject stone);
        void ResetSlot();
        void Highlight();
        void Init(GameBoard gameBoard, SlotLocation location);

        bool Occupied();
    }
}