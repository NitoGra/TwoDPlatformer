using System;
using UnityEngine;

namespace Scripts
{
    internal class Attack : MonoBehaviour
    {
        private int _damage;
        
        public void Instantiate(int damage)
        {
            _damage = damage;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IDamageable>(out var unit))
                unit.TakeDamage(_damage);
        }
    }
}