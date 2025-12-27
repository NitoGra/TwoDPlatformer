using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    internal class Bootstrap : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private HealthView _playerHealth;
        [Space] 
        [SerializeField] private Enemy _enemy;
        [SerializeField] private HealthView _enemyHealth;
        [Space] 
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private LayerMask _groundLayer;
        [Space] 
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private EnemyConfig _enemyConfig;
        [Space] 
        [SerializeField] private AnimationClipSettings _animationClipSettings;
        [Space] 
        [SerializeField] private List<Transform> _patrolTargets = new();

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;

            _player.Init(_playerConfig, 
                _playerHealth.Changed,_animationClipSettings.GetDeadClipLength);
            _enemy.Init(_enemyConfig, _patrolTargets,
                _playerHealth.Changed,_animationClipSettings.GetDeadClipLength);
        }
    }
}