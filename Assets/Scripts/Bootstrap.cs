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
        [SerializeField] private LayerMask _groundLayer;
        [Space]
        [SerializeField] private UnitSettings _playerSettings;
        [SerializeField] private EnemySettings _enemySettings;
        [Space]
        [SerializeField] private List<Transform> _patrolTargets = new();
        
        private void Awake()
        { 
            Cursor.lockState = CursorLockMode.Locked;
            
            _player.Init(
                _playerSettings.MoveSpeed,   _playerSettings.SprintSpeed, 
                _playerSettings.JumpForce, 
                _playerSettings.Damage,  _playerSettings.MaxHealth,
                _groundLayer);

            _enemy.Init(
                _enemySettings.MoveSpeed,
                _enemySettings.JumpForce, 
                _enemySettings.VisualRange, _enemySettings.AttackRange,
                _enemySettings.Damage, _enemySettings.MaxHealth, 
                _playerLayer, _groundLayer,
                _patrolTargets);
        }
    }
}