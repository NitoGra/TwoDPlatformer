using UnityEngine;

namespace Scripts
{
    internal class FirstAidKit : MonoBehaviour, IHealable
    {
        [SerializeField] private int _treatableHealth = 3;

        public int GetHeal() => 
            _treatableHealth;
    }
}