using BoardGame.Utility;
using TMPro;
using UnityEngine;

namespace BoardGame.Core
{
    internal class GameBoardSlot : MonoBehaviour, IGameBoardSlot
    {
        [SerializeField] private Color _highlightColor;
        [SerializeField] private bool _visualizeLocation;
        
        private bool _occupied;
        private Renderer _renderer;
        private GameBoard _gameBoard;
        private SlotLocation _locationOnBoard;
        private GameObject _stone;
        private Color _defaultColor;
        
        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _defaultColor = _renderer.material.color;
        }

        private void OnMouseEnter()
        {
            _renderer.material.color = _highlightColor;
        }

        private void OnMouseExit()
        {
            _renderer.material.color = _defaultColor;
        }

        private void OnMouseDown()
        {
            _gameBoard.BoardSlotClicked(_locationOnBoard);
        }

        public void OccupySlot(GameObject stone)
        {
            _occupied = true;
            _stone = stone;

            Transform slotTransform = transform;
            _stone.transform.position = slotTransform.position;
            _stone.transform.SetParent(slotTransform);
        }

        public void ResetSlot()
        {
            _occupied = false;
            Destroy(_stone);
        }

        public void Highlight()
        {
            _renderer.material.color = Color.yellow;
        }

        public bool Occupied()
        {
            return _occupied;
        }
        
        public void Init(GameBoard gameBoard, SlotLocation locationOnBoard)
        {
            _gameBoard = gameBoard;
            _locationOnBoard = locationOnBoard;
            _occupied = false;

            if (_visualizeLocation)
            {
                GetComponentInChildren<TMP_Text>()
                    .text = _locationOnBoard.x + "," + _locationOnBoard.y;
            }
        }
    }
}