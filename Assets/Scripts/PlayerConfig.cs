using UnityEngine;

namespace Scripts
{ 
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Game2D/Player")]
    internal class PlayerConfig : CharacterConfig
    {
        public float SprintSpeed = 10;
    }
}