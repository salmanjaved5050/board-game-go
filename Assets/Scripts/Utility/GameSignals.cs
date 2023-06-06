using BoardGame.Utility;
using Supyrb;
using Utility;

namespace BoardGame
{
    public static class GameSignals
    {
        public class StartGame : Signal { }

        public class PlayerTurnChanged : Signal<PlayerTurn> { }

        public class PlayerScoreChanged : Signal<PlayerTurn, int> { }

        public class RestartGame : Signal { }

        public class GameWinner : Signal<string> { }

        public class ShowMenu : Signal<MenuType> { }

        public class HideMenu : Signal<MenuType> { }
    }
}