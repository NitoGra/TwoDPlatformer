using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    [Serializable]
    internal class EnemyContext
    {
        private readonly EnemyConfig _enemyConfig;

        private List<Transform> _targets;
        private int _targetIndex = 0;
        private Vector2 _lastPosition;
        private Transform _player;
        private Transform _character;
        private bool _seePlayer = false;
        
        public EnemyContext(List<Transform> targets, EnemyConfig enemyConfig, Transform character)
        {
            _targets = targets;
            _enemyConfig = enemyConfig;
            _character  = character;
        }

        private Vector2 GetPlayerPosition => _player.position;
        private Vector2 GetCurrentPosition => _character.position;
        private float GetDistanceToPlayer => (GetPlayerPosition - GetCurrentPosition).magnitude;
        public Vector2 GetTargetPosition => _seePlayer ? GetPlayerPosition : _targets[_targetIndex].position;
        public bool CanAttack => _seePlayer && GetDistanceToPlayer <= _enemyConfig.AttackRange;

        public void DetectPlayer(bool isRightFacing)
        { 
            if (_player == null)
            {
                Collider2D[] results = Physics2D.OverlapCircleAll(GetCurrentPosition, _enemyConfig.VisualRange, _enemyConfig.PlayerLayer);

                if (results.Length == 0)
                    return;

                _player = results[0].transform;
            }
            else if (GetDistanceToPlayer > _enemyConfig.VisualRange)
            {
                _seePlayer = false;
                _player = null;
                return;
            }
            
            Vector2 directionToPlayer = (GetPlayerPosition - GetCurrentPosition).normalized;
            Vector2 facingDirection = isRightFacing ? Vector2.left : Vector2.right;
            _seePlayer = Vector2.Dot(directionToPlayer, facingDirection) > 0;
        }

        public void TargetCheck()
        {
            if ((GetTargetPosition - GetCurrentPosition).sqrMagnitude < _enemyConfig.NearDistance == false || _seePlayer)
                return;

            _targetIndex++;

            if (_targetIndex >= _targets.Count)
                _targetIndex = 0;
        }

        public bool CanJump()
        {
            bool canJump = (_lastPosition - GetCurrentPosition).sqrMagnitude < 0.0005f;
            _lastPosition = GetCurrentPosition;
            return canJump;
        }
    }
}