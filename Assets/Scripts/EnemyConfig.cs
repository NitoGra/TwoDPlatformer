using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "Game2D/Enemy")]
    internal class EnemyConfig : CharacterConfig
    {        
        public readonly int NearDistance = 1;
        public LayerMask PlayerLayer;
        public float VisualRange = 5;
        public float AttackRange = 5;
    }
}