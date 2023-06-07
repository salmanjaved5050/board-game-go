using Supyrb;

namespace BoardGame.Utility
{
    public static class GameSignals
    {
        public class StartGame : Signal { }

        public class PlayerTurnChanged : Signal<PlayerTurn> { }

        public class PlayerScoreChanged : Signal<PlayerTurn, int> { }

        public class RestartGame : Signal { }

        public class ForfeitGame : Signal { }

        public class GameWinner : Signal<string> { }

        public class ShowMenu : Signal<MenuType> { }

        public class HideMenu : Signal<MenuType> { }
    }
}