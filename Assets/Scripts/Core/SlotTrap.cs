using System.Collections.Generic;
using BoardGame.Utility;
using UnityEngine;

namespace BoardGame.Core
{
    internal class SlotTrap
    {
        private SlotLocation _trapOriginLocation;
        private List<SlotLocation> _trapOffsets;
        private SlotLocation _orthognalOffset;
        private Vector2Int _boardSize;
        private List<SlotLocation> _trapBoundries;
        private SlotLocation _trapTargetLocation;

        /// <summary>
        /// Creates a trap from a given slot location in the direction
        /// depending on the offsets from the slot location
        /// </summary>
        /// <param name="slotLocation">The origin location of the trap from where boundries are calculated</param>
        /// <param name="boardSize">The size of the game board</param>
        /// <param name="trapOffsets">The offsets from which slot locations are calculated and boundries are defined</param>
        /// <param name="orthognalOffset">The slot location of potential target stone to be captured, belonging to enemy</param>
        internal SlotTrap(SlotLocation slotLocation, Vector2Int boardSize,List<SlotLocation> trapOffsets, SlotLocation orthognalOffset)
        {
            _trapOriginLocation = slotLocation;
            _boardSize = boardSize;
            _trapOffsets = trapOffsets;
            _orthognalOffset = orthognalOffset;
            
            InitializeTrap();
        }

        private bool IsSlotOnBoard(SlotLocation slotLocation)
        {
            return (slotLocation.x >= 0 && slotLocation.y >= 0 && slotLocation.x < _boardSize.x && slotLocation.y < _boardSize.y);
        }

        private List<SlotLocation> CreateTrapBoundriesFromOrigin()
        {
            List<SlotLocation> slots = new List<SlotLocation>();
            for (int i = 0; i < _trapOffsets.Count; i++)
            {
                SlotLocation neighbour = new SlotLocation(_trapOriginLocation.x + _trapOffsets[i]
                    .x, _trapOriginLocation.y + _trapOffsets[i]
                    .y);

                if (IsSlotOnBoard(neighbour))
                {
                    slots.Add(neighbour);
                }
            }

            return slots;
        }
        
        private void InitializeTrap()
        {
            _trapBoundries = CreateTrapBoundriesFromOrigin();
        }

        internal List<SlotLocation> GetTrapBoundries() => _trapBoundries;

        internal SlotLocation GetTargetLocation()
        {
            SlotLocation targetLocation = new(_trapOriginLocation.x + _orthognalOffset.x, _trapOriginLocation.y + _orthognalOffset.y);
            return targetLocation;
        }
    }
}