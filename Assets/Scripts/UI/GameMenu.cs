using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{

    public void QuitGame() => Application.Quit();

    public void StartGame()
    {
        GameManager.Instance.StartGame();
        InputManager.Instance.SwitchInputActionMap(InputActionType.Player);
        GameUIManager.Instance.HideMenu();
    }

    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
        GameUIManager.Instance.HideMenu();
    }

    public void ReturnToMenu()
    {
        GameManager.Instance.ReturnToMenu();
        GameUIManager.Instance.HideMenu();
    }
}
