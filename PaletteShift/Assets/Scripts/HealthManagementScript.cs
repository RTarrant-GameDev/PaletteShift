using UnityEngine;

public class HealthManagementScript : MonoBehaviour {
    private HealthScript HealthModel;
    [SerializeField] private int HealthValue;
    [SerializeField] private HealthUIScript HealthView;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        HealthModel = new HealthScript(HealthValue);
        HealthModel.OnHealthChanged += HealthView.SetHealth;

        //Initial sync
        HealthView.SetHealth(HealthModel.CurrentHealth, HealthModel.MaxHealth);
    }

    public void HealPlayer(int amount) => HealthModel.Heal(amount);
    public void DamagePlayer(int amount) => HealthModel.TakeDamage(amount);
}
