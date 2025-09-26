using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartFunction() {
        SceneManager.LoadScene("DemoScene");
    }

    public void QuitFunction() {
        Application.Quit();
    }
}
