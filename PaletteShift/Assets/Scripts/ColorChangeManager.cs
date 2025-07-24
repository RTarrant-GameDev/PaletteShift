using UnityEngine;
using System.Collections;

public class ColorChangeManager : MonoBehaviour
{
    public GameObject ColorChangeObject;

#region "Serialized Fields
    [SerializeField]
    float ColorChangeTime;

    [SerializeField]
    Color DefaultColor = Color.black;
#endregion

    private Coroutine ColorRoutine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerColorChange(Color.red);
        }
    }

    public void TriggerColorChange(Color ColorToChange)
    {
        if (ColorRoutine != null)
            StopCoroutine(ColorRoutine);
        ColorRoutine = StartCoroutine(ChangeColorForDuration(ColorToChange));
    }
    
    private IEnumerator ChangeColorForDuration(Color NewColor)
    {
        ColorChangeObject.GetComponent<ColorChangeMechanic>().ChangeColor(NewColor);
        yield return new WaitForSeconds(ColorChangeTime);
        ColorChangeObject.GetComponent<ColorChangeMechanic>().ChangeColor(DefaultColor);
    }
}
