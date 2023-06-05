using System.Collections.Generic;

namespace BoardGame.Utility
{
    public static class GameConstants
    {
        public static class BoardGameOffsets
        {
            public static SlotLocation LeftOrthognalSlotOffset = new SlotLocation(0, -1);
            public static SlotLocation RightOrthognalSlotOffset = new SlotLocation(0, 1);
            public static SlotLocation TopOrthognalSlotOffset = new SlotLocation(-1, 0);
            public static SlotLocation BottomOrthognalSlotOffset = new SlotLocation(1, 0);

            public static readonly List<SlotLocation> LeftTrapOffsets = new List<SlotLocation>()
            {
                new SlotLocation(-1, -1),
                new SlotLocation(0, -2),
                new SlotLocation(1, -1)
            };
            
            public static readonly List<SlotLocation> RightTrapOffsets = new List<SlotLocation>()
            {
                new SlotLocation(-1, 1),
                new SlotLocation(0, 2),
                new SlotLocation(1, 1)
            };
            
            public static readonly List<SlotLocation> TopTrapOffsets = new List<SlotLocation>()
            {
                new SlotLocation(-1, -1),
                new SlotLocation(-2, 0),
                new SlotLocation(-1, 1)
            };
            
            public static readonly List<SlotLocation> BottomTrapOffsets = new List<SlotLocation>()
            {
                new SlotLocation(1, -1),
                new SlotLocation(2, 0),
                new SlotLocation(1, 1)
            };
        }
    }
}