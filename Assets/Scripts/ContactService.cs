using UnityEngine;

namespace Scripts
{
    internal static class ContactService
    {
        public static void ContactCheck(Collision2D other, HealthModel health)
        {
            if (other.collider.TryGetComponent(out IHealabling healabling) == false) 
                return;
                
            if(health.CurrentHealth >= health.MaxHealth)
                return;
                    
            health.Heal(healabling.Heal());
            MonoBehaviour.Destroy(other.gameObject);
        }
    }
}