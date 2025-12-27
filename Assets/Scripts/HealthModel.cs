using System;
using UnityEngine;

namespace Scripts
{
    internal class HealthModel
    {
        public event Action Died;
        public event Action<int,int> Changed;
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; }
        
        public HealthModel(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }
        
        public void TakeDamage(int damage)
        {
            if (damage <= 0)
                return;

            if (CurrentHealth <= 0)
                return;

            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
            Changed?.Invoke(CurrentHealth, MaxHealth);

            if (CurrentHealth <= 0)
                Died?.Invoke();
        }

        public void Heal(int health)
        {
            if(health <= 0)
                return;
            
            CurrentHealth = Mathf.Clamp(CurrentHealth + health, 0, MaxHealth);
            Changed?.Invoke(CurrentHealth, MaxHealth);
        }
    }
}