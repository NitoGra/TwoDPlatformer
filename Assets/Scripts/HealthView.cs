using UnityEngine;

namespace Scripts
{
    internal class HealthView : MonoBehaviour
    {
        public void Changed(int currentHealth, int maxHealth)
        {
            Debug.Log($"Health: {currentHealth} Changed: {maxHealth} ");
        }
    }
}