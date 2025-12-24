using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    internal class Bootstrap : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Enemy _enemy;
        [Space]
        [SerializeField]private  LayerMask _playerLayer;
        [Space]
        [SerializeField] private UnitSettings _playerSettings;
        [SerializeField] private EnemySettings _enemySettings;
        [Space]
        [SerializeField] private List<Transform> _patrolTargets = new();
        
        private void Awake()
        { 
            Cursor.lockState = CursorLockMode.Locked;
            
            _player.Init(
                _playerSettings.MoveSpeed, 
                _playerSettings.SprintSpeed, 
                _playerSettings.JumpForce, 
                _playerSettings.MaxHealth,
                _playerSettings.Damage);
            
            _enemy.Init(
                _enemySettings.MoveSpeed, 
                _enemySettings.SprintSpeed, 
                _enemySettings.JumpForce, 
                _enemySettings.MaxHealth,
                _patrolTargets,
                _enemySettings.VisualRange,
                _enemySettings.AttackRange,
                _enemySettings.Damage,
                _playerLayer);
        }
    }
}