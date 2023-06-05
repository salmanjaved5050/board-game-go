using Supyrb;
using Utility;

namespace BoardGame
{
    public static class GameSignals
    {
        public class StartGame : Signal { }

        public class PlayerTurnChanged : Signal<PlayerTurn> { }

        public class PlayerScoreChanged : Signal<PlayerTurn, int> { }

        public class GameFinished : Signal<string> { }

        public class RestartGame : Signal { }
    }
}