using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    internal class TurningOffController :MonoBehaviour
    {
        [SerializeField] private List<GameObject> _objects;
        
        public void TurningOffFromAnimation()
        {
            foreach (var obj in _objects)
                obj.SetActive(false);
        }
    }
}