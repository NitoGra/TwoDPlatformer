using System;
using UnityEngine;

namespace Scripts
{
    [Serializable]
    internal class Health
    {
        public event Action OnDeath;

        public Health(int maxHealth)
        {
            MaxHealth = maxHealth;
            TempHealth = maxHealth;
        }

        public int TempHealth { get; private set; }
        public int MaxHealth { get; }

        public void TakeDamage(int damage)
        {
            Debug.Log($"Health: {TempHealth} TakeDamage: {damage} ");

            if (TempHealth <= 0)
                return;

            TempHealth -= damage;

            if (TempHealth > 0)
                return;

            OnDeath?.Invoke();
            TempHealth = 0;
        }

        public void Heal(int health)
        {
            Debug.Log($"Health: {TempHealth} TakeHeal: {health} ");
            TempHealth += health;

            if (TempHealth > MaxHealth)
                TempHealth = MaxHealth;
        }
    }
}