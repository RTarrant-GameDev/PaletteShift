using System;
using UnityEngine;

public class HealthScript {
    public int MaxHealth { get; private set; }
    private int currentHealth;

    public int CurrentHealth {
        get => currentHealth;
        private set {
            currentHealth = Mathf.Clamp(value, 0, MaxHealth);
            OnHealthChanged?.Invoke(currentHealth, MaxHealth);
        }
    }

    public event Action<int, int> OnHealthChanged;

    public HealthScript(int maxHealth) {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int DamageToTake) {
        CurrentHealth -= DamageToTake;
    }

    public void Heal(int HealthToHeal) {
        CurrentHealth += HealthToHeal;
    }
}
