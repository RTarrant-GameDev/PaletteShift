using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartFunction() {
        SceneManager.LoadScene("DemoScene");
        GameSystemManagerScript.GameSystemManagerInstance.ChangeState(GameState.Gameplay);
    }

    public void QuitFunction() {
        Application.Quit();
    }
}
