using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorChangeManager : MonoBehaviour
{
    public static ColorChangeManager ManagerInstance { get; private set; }
    public GameObject ColorChangeObject;

#region "Serialized Fields
    [SerializeField]
    float ColorChangeTime;

    [SerializeField]
    Color DefaultColor = Color.black;
#endregion

    private Coroutine ColorRoutine;

    public event Action<Color, float> OnColorChange;

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Awake() {
        if (ManagerInstance != null && ManagerInstance != this) { //if there is already an instance, destroy this instance
            Destroy(this);
            return;
        }

        ManagerInstance = this; //assuming that there is no pre-existing TimerInstance, make this the current TimerInstance
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        SetPlayer();
    }

    public void SetPlayer() {
        if(GameObject.FindWithTag("Player")) {
            ColorChangeObject = GameObject.FindWithTag("Player"); 
        }
    }

    public void TriggerColorChange(Color ColorToChange) {
        ColorRoutine = StartCoroutine(ChangeColorForDuration(ColorToChange));
        OnColorChange?.Invoke(ColorToChange, ColorChangeTime);
    }

    public void ResetColor() {
        ColorChangeObject.GetComponent<ColorChangeMechanic>().ChangeColor(DefaultColor);
        StopCoroutine("ChangeColorForDuration");
    }
    
    private IEnumerator ChangeColorForDuration(Color NewColor) {
        ColorChangeObject.GetComponent<ColorChangeMechanic>().ChangeColor(NewColor);
        yield return new WaitForSeconds(ColorChangeTime);
        ResetColor();
    }
}
