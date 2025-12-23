using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    internal class Bootstrap : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Enemy _enemy;
        [Space]
        [SerializeField] private UnitSettings _playerSettings;
        [SerializeField] private EnemySettings _enemySettings;
        [Space]
        [SerializeField] private List<Transform> _patrolTargets = new();
        
        private void Awake()
        {
            _player.Instantiate(
                _playerSettings.MoveSpeed, 
                _playerSettings.SprintSpeed, 
                _playerSettings.JumpForce, 
                _playerSettings.MaxHealth);
            
            _enemy.Instantiate(
                _enemySettings.MoveSpeed, 
                _enemySettings.SprintSpeed, 
                _enemySettings.JumpForce, 
                _enemySettings.MaxHealth,
                _patrolTargets);
        }
    }
}