using UnityEngine;

namespace Scripts
{
    internal class FirstAidKit : MonoBehaviour
    {
        [SerializeField] private int _treatableHealth = 3;

        public void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent<IHealable>(out var unit) == false)
                return;

            if (unit.TryHeal(_treatableHealth))
                Destroy(gameObject);
        }
    }
}