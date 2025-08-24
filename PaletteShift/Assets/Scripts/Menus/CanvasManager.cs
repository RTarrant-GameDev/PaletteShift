using System;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{

    public static CanvasManager CanvasManagerInstance { get; private set; }
    public GameObject PlayerHUD;
    public GameObject PauseMenu;

    [SerializeField]
    private bool PauseMenuActive;

    public event Action Pause;
    public event Action Resume;

    // make it so that, when level starts, only HUD is visible to player
    void Start()
    {
        if (CanvasManagerInstance != null && CanvasManagerInstance != this)
        { //if there is already an instance, destroy this instance
            Destroy(this);
            return;
        }

        CanvasManagerInstance = this;

        PlayerHUD.SetActive(true);
        PauseMenu.SetActive(false);
    }

    public void PauseGame()
    {
        PlayerHUD.SetActive(false);
        PauseMenu.SetActive(true);

        Time.timeScale = 0;
        Pause?.Invoke(); // fire pause event
    }

    public void ResumeGame()
    {
        PlayerHUD.SetActive(true);
        PauseMenu.SetActive(false);

        Time.timeScale = 1;
        Resume?.Invoke(); // fire resume event
    }
}
