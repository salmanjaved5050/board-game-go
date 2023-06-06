using BoardGame.Utility;
using UnityEngine;

namespace BoardGame.Ui
{
    public class UiMenu : MonoBehaviour
    {
        [SerializeField] private MenuType menuType;

        public void ShowMenu()
        {
            gameObject.SetActive(true);
        }

        public void HideMenu()
        {
            gameObject.SetActive(false);
        }

        public MenuType GetMenuType()
        {
            return menuType;
        }

        public virtual void ResetMenu() { }
    }
}