using UnityEngine;

public class HealthManagementScript : MonoBehaviour
{
    private HealthScript HealthModel;
    [SerializeField] private HealthUIScript HealthView;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HealthModel = new HealthScript(10);
        HealthModel.OnHealthChanged += HealthView.SetHealth;

        //Initial sync
        HealthView.SetHealth(HealthModel.CurrentHealth, HealthModel.MaxHealth);

        //Simulate damage
        InvokeRepeating(nameof(SimulateDamage), 2f, 2f);
    }

    void SimulateDamage()
    {
        HealthModel.TakeDamage(Random.Range(1, 3));
    }

    public void HealPlayer(int amount) => HealthModel.Heal(amount);
    public void DamagePlayer(int amount) => HealthModel.TakeDamage(amount);
}
