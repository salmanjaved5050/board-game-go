using System.Collections.Generic;
using BoardGame.Utility;
using UnityEngine;

namespace BoardGame.Core
{
    internal class SlotTrap
    {
        private SlotLocation _trapOriginLocation;
        private Vector2Int _boardSize;
        private List<SlotLocation> _trapBoundries;
        private SlotLocation _trapTargetLocation;

        protected List<SlotLocation> trapOffsets;
        protected SlotLocation orthognalOffset;

        internal SlotTrap(SlotLocation location, Vector2Int boardLimits)
        {
            _trapOriginLocation = location;
            _boardSize = boardLimits;
        }

        private bool IsSlotOnBoard(SlotLocation slotLocation)
        {
            return (slotLocation.x >= 0 && slotLocation.y >= 0 && slotLocation.x < _boardSize.x && slotLocation.y < _boardSize.y);
        }

        private List<SlotLocation> CreateTrapAroundOrigin()
        {
            List<SlotLocation> slots = new List<SlotLocation>();
            for (int i = 0; i < trapOffsets.Count; i++)
            {
                SlotLocation neighbour = new SlotLocation(_trapOriginLocation.x + trapOffsets[i]
                    .x, _trapOriginLocation.y + trapOffsets[i]
                    .y);

                if (IsSlotOnBoard(neighbour))
                {
                    slots.Add(neighbour);
                }
            }

            return slots;
        }

        protected virtual void GenerateTrapOffsets() { }

        protected virtual void InitializeTrap() { }

        protected void SetupBoundries()
        {
            _trapBoundries = CreateTrapAroundOrigin();
        }
        
        internal List<SlotLocation> GetTrapBoundries() => _trapBoundries;

        internal SlotLocation GetTargetLocation()
        {
            SlotLocation targetLocation = new(_trapOriginLocation.x + orthognalOffset.x, _trapOriginLocation.y + orthognalOffset.y);
            return IsSlotOnBoard(targetLocation) ? targetLocation : null;
        }
    }

    internal class SlotTrapLeft : SlotTrap
    {
        internal SlotTrapLeft(SlotLocation location, Vector2Int boardLimits) : base(location, boardLimits)
        {
            InitializeTrap();
        }

        protected sealed override void InitializeTrap()
        {
            GenerateTrapOffsets();
            SetupBoundries();
        }

        protected sealed override void GenerateTrapOffsets()
        {
            trapOffsets = new List<SlotLocation>()
            {
                new SlotLocation(-1, -1),
                new SlotLocation(0, -2),
                new SlotLocation(1, -1)
            };
            orthognalOffset = new SlotLocation(0, -1);
        }
    }
    
    internal class SlotTrapRight : SlotTrap
    {
        internal SlotTrapRight(SlotLocation location, Vector2Int boardLimits) : base(location, boardLimits)
        {
            InitializeTrap();
        }

        protected sealed override void InitializeTrap()
        {
            GenerateTrapOffsets();
            SetupBoundries();
        }

        protected sealed override void GenerateTrapOffsets()
        {
            trapOffsets = new List<SlotLocation>()
            {
                new SlotLocation(-1, 1),
                new SlotLocation(0, 2),
                new SlotLocation(1, 1)
            };
            orthognalOffset = new SlotLocation(0, 1);
        }
    }
    
    internal class SlotTrapTop : SlotTrap
    {
        internal SlotTrapTop(SlotLocation location, Vector2Int boardLimits) : base(location, boardLimits)
        {
            InitializeTrap();
        }

        protected sealed override void InitializeTrap()
        {
            GenerateTrapOffsets();
            SetupBoundries();
        }

        protected sealed override void GenerateTrapOffsets()
        {
            trapOffsets = new List<SlotLocation>()
            {
                new SlotLocation(-1, -1),
                new SlotLocation(-2, 0),
                new SlotLocation(-1, 1)
            };
            orthognalOffset = new SlotLocation(-1,0);
        }
    }
    
    internal class SlotTrapBottom : SlotTrap
    {
        internal SlotTrapBottom(SlotLocation location, Vector2Int boardLimits) : base(location, boardLimits)
        {
            InitializeTrap();
        }

        protected sealed override void InitializeTrap()
        {
            GenerateTrapOffsets();
            SetupBoundries();
        }

        protected sealed override void GenerateTrapOffsets()
        {
            trapOffsets = new List<SlotLocation>()
            {
                new SlotLocation(1, -1),
                new SlotLocation(2, 0),
                new SlotLocation(1, 1)
            };
            orthognalOffset = new SlotLocation(1,0);
        }
    }
}