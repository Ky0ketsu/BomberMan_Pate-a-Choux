using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public void STARTGAME()
    {
        MENU.SCRIPT.NewGame();
    }

    public void Lobby()
    {
        MENU.SCRIPT.LobbyMenu();
    }

    public void SETTINGS()
    {
        MENU.SCRIPT.ShowSettingsFromMainMenu();
    }

    public void CREDITS()
    {
        MENU.SCRIPT.ShowCreditsFromMainMenu();
    }

    public void QUIT()
    {
        MENU.SCRIPT.QuitApp();
    }
    
} // SCRIPT END
