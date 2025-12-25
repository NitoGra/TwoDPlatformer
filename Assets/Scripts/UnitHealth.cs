using System;
using UnityEngine;

namespace Scripts
{
    [Serializable]
    internal class UnitHealth : MonoBehaviour, IDamageable, IHealable
    {
        private Health _health;

        public void Init(int health, Action onDeath)
        {
            _health = new(health);
            _health.OnDeath += onDeath;
        }
        
        public void TakeDamage(int damage) 
            => _health.TakeDamage(damage);

        public bool TryHeal(int heal)
        {
            if (_health.TempHealth >= _health.MaxHealth ||  _health.TempHealth <= 0)
                return false;
            
            _health.Heal(heal);
            return true;
        }
    }
}