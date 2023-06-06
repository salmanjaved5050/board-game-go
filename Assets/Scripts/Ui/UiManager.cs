using System.Collections.Generic;
using System.Linq;
using BoardGame.Utility;
using Supyrb;
using UnityEngine;

namespace BoardGame.Ui
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private MenuType defaultMenu;

        private List<UiMenu> _uiMenus;
        private UiMenu _currentMenu;

        private void Awake()
        {
            InitializeUi();
        }

        private void OnEnable()
        {
            Signals.Get<GameSignals.HideMenu>()
                .AddListener(HideMenu);

            Signals.Get<GameSignals.ShowMenu>()
                .AddListener(ShowMenu);

            Signals.Get<GameSignals.RestartGame>()
                .AddListener(ResetUiMenus);
        }

        private void Start()
        {
            for (int i = 0; i < _uiMenus.Count; i++)
            {
                _uiMenus[i]
                    .HideMenu();
            }

            ShowMenu(defaultMenu);
        }

        private void ResetUiMenus()
        {
            for (int i = 0; i < _uiMenus.Count; i++)
            {
                _uiMenus[i].ResetMenu();
            }
        }

        private void OnDisable()
        {
            Signals.Get<GameSignals.HideMenu>()
                .RemoveListener(HideMenu);

            Signals.Get<GameSignals.ShowMenu>()
                .RemoveListener(ShowMenu);
            
            Signals.Get<GameSignals.RestartGame>()
                .AddListener(ResetUiMenus);
        }

        private void InitializeUi()
        {
            _uiMenus = GetComponentsInChildren<UiMenu>(true)
                .ToList();

            if (!_uiMenus.Any()) Debug.LogError("No Ui Menus Found In Scene.");

            List<MenuType> dups = _uiMenus.GroupBy(menu => menu.GetMenuType())
                .Where(group => group.Count() > 1)
                .Select(menu => menu.Key)
                .ToList();

            if (!dups.Any()) return;

            for (int i = 0; i < dups.Count; i++)
                Debug.LogError("Found Duplicate Menu : " + dups[i]);
        }

        private UiMenu GetMenu(MenuType menuName)
        {
            return _uiMenus.Find(menu => menu.GetMenuType() == menuName);
        }

        private void HideCurrentMenu()
        {
            if (!_currentMenu) return;
            _currentMenu.HideMenu();
        }

        private void ShowMenu(MenuType menuName)
        {
            UiMenu menu = GetMenu(menuName);

            if (menu)
            {
                HideCurrentMenu();
                menu.ShowMenu();
                _currentMenu = menu;
            }
            else
            {
                Debug.LogError("No Menu Found With Name : " + menuName);
            }
        }

        private void HideMenu(MenuType menuType)
        {
            UiMenu menu = GetMenu(menuType);
            if (menu)
                menu.HideMenu();
            else
                Debug.LogError("No Menu Found With Name : " + menuType);
        }
    }
}