using BoardGame.Core.ScriptableObjects;
using BoardGame.Utility;
using Supyrb;
using UnityEngine;

namespace BoardGame.Core
{
    /// <summary>
    /// This class controls the board slots,spawns them and updates them
    /// according to the game state coming from game logic.
    /// </summary>
    internal class GameBoard : MonoBehaviour
    {
        [SerializeField] private GameBoardConfig _gameBoardConfig;

        private IGameBoardSlot[,] _boardSlots;
        private GameLogic _gameLogic;

        private void OnEnable()
        {
            Signals.Get<GameSignals.StartGame>()
                .AddListener(InitializeGameBoard);
            Signals.Get<GameSignals.RestartGame>()
                .AddListener(ResetGameBoard);
            Signals.Get<GameSignals.ForfeitGame>()
                .AddListener(OnPlayerForfeitGame);
        }

        private void OnDisable()
        {
            Signals.Get<GameSignals.StartGame>()
                .RemoveListener(InitializeGameBoard);
            Signals.Get<GameSignals.RestartGame>()
                .RemoveListener(ResetGameBoard);
            Signals.Get<GameSignals.ForfeitGame>()
                .RemoveListener(OnPlayerForfeitGame);
        }

        private void SpawnBoardSlots()
        {
            Vector2Int boardSize = _gameBoardConfig.GameBoardSize;
            for (int x = 0; x < boardSize.x; x++)
            {
                for (int z = 0; z < boardSize.y; z++)
                {
                    _boardSlots[x, z] = CreateBoardSlot(new Vector3(z, 0, x));
                }
            }
        }

        private void ResetBoardSlots()
        {
            int xSize = _boardSlots.GetLength(0);
            int ySize = _boardSlots.GetLength(1);

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    _boardSlots[x, y]
                        .ResetSlot();
                }
            }
        }

        private GameBoardSlot CreateBoardSlot(Vector3 position)
        {
            GameObject slotGameObject = Instantiate(_gameBoardConfig.BoardSlotPrefab, transform);
            slotGameObject.transform.localPosition = position;
            slotGameObject.transform.name = "Slot [" + position.x + "," + position.z + "]";

            IGameBoardSlot slot = slotGameObject.GetComponent<IGameBoardSlot>();
            SlotLocation slotLocation = new SlotLocation((int)position.z, (int)position.x);

            slot.Init(this, slotLocation);

            return slot as GameBoardSlot;
        }

        private void OnPlayerForfeitGame()
        {
            Signals.Get<GameSignals.ShowMenu>()
                .Dispatch(MenuType.WinMenu);
            Signals.Get<GameSignals.GameWinner>()
                .Dispatch(_gameLogic.GetWinner(true));
        }

        private void InitializeGameBoard()
        {
            if (_gameBoardConfig == null)
            {
                Debug.LogError("Need Game Board Configuration!");
                return;
            }

            _boardSlots = new IGameBoardSlot[_gameBoardConfig.GameBoardSize.x, _gameBoardConfig.GameBoardSize.y];
            _gameLogic = new GameLogic(_gameBoardConfig.GameBoardSize);
            SpawnBoardSlots();

            Signals.Get<GameSignals.PlayerTurnChanged>()
                .Dispatch(_gameLogic.GetCurrentPlayerTurn());
        }

        private void ResetGameBoard()
        {
            ResetBoardSlots();
            _gameLogic.ResetGameLogic();
            Signals.Get<GameSignals.PlayerTurnChanged>()
                .Dispatch(PlayerTurn.Player1);
        }

        private void UpdateSlotState(IGameBoardSlot boardSlot, BoardSlotState slotState)
        {
            bool isSlotOccupied = boardSlot.Occupied();
            if (slotState.GetOwner() == BoardSlotOwner.None)
            {
                if (isSlotOccupied)
                {
                    boardSlot.ResetSlot();
                }

                return;
            }

            if (!isSlotOccupied)
            {
                GameObject stone = slotState.GetOwner() == BoardSlotOwner.Player1
                    ? Instantiate(_gameBoardConfig.Player1StonePrefab)
                    : Instantiate(_gameBoardConfig.Player2StonePrefab);

                boardSlot.OccupySlot(stone);
            }
        }

        private void ApplyBoardState(BoardSlotState[,] state)
        {
            int xSize = state.GetLength(0);
            int ySize = state.GetLength(1);

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    // get current slot and its state coming from logic then update the slot  
                    IGameBoardSlot boardSlot = _boardSlots[x, y];
                    BoardSlotState slotState = state[x, y];
                    UpdateSlotState(boardSlot, slotState);
                }
            }
        }

        private void DisableBoardSlots()
        {
            int xSize = _boardSlots.GetLength(0);
            int ySize = _boardSlots.GetLength(1);

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    IGameBoardSlot boardSlot = _boardSlots[x, y];
                    boardSlot.Disable();
                }
            }
        }

        internal void BoardSlotClicked(SlotLocation location)
        {
            if (_gameLogic.IsMoveValidAtLocation(location))
            {
                // if a move is valid at current slot location, then after its done get the latest state of the board
                BoardSlotState[,] state = _gameLogic.GetBoardState();
                ApplyBoardState(state);

                if (_gameLogic.IsGameFinished())
                {
                    DisableBoardSlots();
                    Signals.Get<GameSignals.ShowMenu>()
                        .Dispatch(MenuType.WinMenu);
                    Signals.Get<GameSignals.GameWinner>()
                        .Dispatch(_gameLogic.GetWinner());
                }
                else
                {
                    Signals.Get<GameSignals.PlayerTurnChanged>()
                        .Dispatch(_gameLogic.GetCurrentPlayerTurn());
                }
            }
            else
            {
                Debug.LogError("Invalid Move!");
            }
        }
    }
}