using UnityEngine;

namespace Scripts
{
    internal class Coin : MonoBehaviour
    {
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.GetComponent<Player>() != null)
                Destroy(gameObject);
        }
    }
}