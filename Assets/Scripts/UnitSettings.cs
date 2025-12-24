using System;
using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu(fileName = "UnitSettings", menuName = "Game2D/Unit")]
    internal class UnitSettings : ScriptableObject
    {
        public float MoveSpeed = 10;
        public float SprintSpeed = 10;
        public float JumpForce = 10;
        public int MaxHealth = 10;
        public int Damage = 1;
    }
}