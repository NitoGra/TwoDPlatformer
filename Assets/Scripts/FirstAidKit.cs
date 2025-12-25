using UnityEngine;

namespace Scripts
{
    internal class FirstAidKit : MonoBehaviour
    {
        [SerializeField] private int _treatableHealth = 3;

        public void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent(out IHealable healable) == false)
                return;

            if (healable.TryHeal(_treatableHealth))
                Destroy(gameObject);
        }
    }
}