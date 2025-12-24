using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "Game2D/Enemy")]
    internal class EnemySettings : UnitSettings
    {
        public float VisualRange = 5;
        public float AttackRange = 5;
    }
}