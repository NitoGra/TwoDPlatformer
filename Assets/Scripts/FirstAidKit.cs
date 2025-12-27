using UnityEngine;

namespace Scripts
{
    internal class FirstAidKit : MonoBehaviour, IHealabling
    {
        [SerializeField] private int _treatableHealth = 3;

        public int Heal() => _treatableHealth;
    }
}