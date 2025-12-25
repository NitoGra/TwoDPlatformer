using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    [Serializable]
    internal class Navigation
    {
        private LayerMask _playerLayer;
        private List<Transform> _targets;
        private int _targetIndex = 0;
        private float _attackRange;
        private Vector2 _lastPosition;
        private float _visualRange;
        private Transform _player;
        private bool _iSeePlayer = false;
        
        public Vector2 GetTargetPosition => _iSeePlayer ? GetPlayerPosition : _targets[_targetIndex].position;
        
        private Vector2 GetPlayerPosition => _player.position;
        
        private float GetDistanceToPlayer(Vector2 position) => (position - GetPlayerPosition).magnitude;

        public Navigation(float attackRange, List<Transform> targets,
            float visualRange, LayerMask playerLayer)
        {
            _targets = targets;
            _visualRange = visualRange;
            _playerLayer = playerLayer;
            _attackRange = attackRange;
        }

        public bool DoYouNeedAttack(Vector2 position, bool isRightFacing)
        {
            TargetCheck(position);
            
            if (_player == null)
            {
                Collider2D[] results = Physics2D.OverlapCircleAll(position, _visualRange, _playerLayer);

                if (results.Length == 0)
                    return false;

                _player = results[0].transform;
            }

            if (GetDistanceToPlayer(position) > _visualRange)
            {
                _iSeePlayer = false;
                _player = null;
                return false;
            }
            
            Vector2 directionToPlayer = (GetPlayerPosition - position).normalized;
            Vector2 facingDirection = isRightFacing ? Vector2.left : Vector2.right;

            _iSeePlayer = Vector2.Dot(directionToPlayer, facingDirection) > 0;
            return _iSeePlayer && GetDistanceToPlayer(position) <= _attackRange;
        }

        public void TargetCheck(Vector2 position)
        {
            if ((GetTargetPosition - position).sqrMagnitude < 1 == false || _iSeePlayer)
                return;

            _targetIndex++;

            if (_targetIndex >= _targets.Count)
                _targetIndex = 0;
        }

        public bool DoINeedJump(Vector2 position)
        {
            bool isJumpNeed = _iSeePlayer == false && (_lastPosition - position).sqrMagnitude < 0.0005f;
            _lastPosition = position;
            return isJumpNeed;
        }
    }
}