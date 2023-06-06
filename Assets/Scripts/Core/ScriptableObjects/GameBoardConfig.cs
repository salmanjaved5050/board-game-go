using UnityEngine;

namespace BoardGame.Core.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameBoardConfig", menuName = "BoardGame/GameBoardConfig")]
    internal class GameBoardConfig : ScriptableObject
    {
        public Vector2Int GameBoardSize;
        public GameObject BoardSlotPrefab;
        public GameObject Player1StonePrefab;
        public GameObject Player2StonePrefab;
    }
}