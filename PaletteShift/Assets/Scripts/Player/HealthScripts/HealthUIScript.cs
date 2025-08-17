using UnityEngine;
using UnityEngine.UI;

public class HealthUIScript : MonoBehaviour {
    [SerializeField] private Slider HealthbarSlider;

    public void SetHealth(int CurrentHP, int MaxHP) {
        if (HealthbarSlider != null) {
            HealthbarSlider.maxValue = MaxHP;
            HealthbarSlider.value = CurrentHP;
        }
    }
}
