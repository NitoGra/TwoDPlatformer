using System;
using UnityEngine;

namespace Scripts
{
    internal class CharacterConfig : ScriptableObject
    {
        public float MoveSpeed = 10;
        public float JumpForce = 10;
        public int MaxHealth = 10;
        public int Damage = 1;
        public LayerMask GroundLayer = 1;
    }
}