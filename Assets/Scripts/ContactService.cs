using UnityEngine;

namespace Scripts
{
    internal static class ContactService
    {
        public static void ContactCheck(Collision2D other, HealthModel health)
        {
            if (other.collider.TryGetComponent(out IHealable healable) == false) 
                return;
                
            if(health.CurrentHealth >= health.MaxHealth)
                return;
                    
            health.Heal(healable.GetHeal());
            MonoBehaviour.Destroy(other.gameObject);
        }
    }
}