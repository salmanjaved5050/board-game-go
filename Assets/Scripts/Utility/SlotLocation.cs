namespace BoardGame.Utility
{
    public struct SlotLocation
    {
        private int _x;
        private int _y;

        public int x => _x;
        public int y => _y;

        public SlotLocation(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public override string ToString()
        {
            return _x + "," + _y;
        }
    }
}