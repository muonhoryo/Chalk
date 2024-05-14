
using Chalk.Exceptions;
using Chalk.Player;
using UnityEngine;

namespace Chalk.MainMenu
{
    public sealed class MainMenuUI : MonoBehaviour
    {
        public void StartGame()
        {
            PlayerController.instance_.IsActive_= true;
            Cursor.visible = false;
            gameObject.SetActive(false);
        }
        public void ExitGame()
        {
            SL.SL_Executor.Command_Quit();
        }
        public void OpenSubmenu(GameObject submenu)
        {
            if (submenu == null)
                throw new System.NullReferenceException("Missing Submenu.");

            submenu.SetActive(true);
        }
        public void CloseSubmenu(GameObject submenu)
        { 
            if (submenu == null)
                throw new System.NullReferenceException("Missing Submenu.");

            submenu.SetActive(false);
        }
    }
}
