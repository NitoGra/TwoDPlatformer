using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    [Serializable]
    internal class Navigation
    {
        private readonly Action _jump;
        private readonly Action _attack;
        private LayerMask _playerLayer;
        private List<Transform> _targets;
        private int _targetIndex = 0;
        private float _attackRange;
        private Vector2 _lastPosition;
        private float _visualRange;
        private Transform _player;
        private bool _iSeePlayer = false;

        public Vector2 GetTargetPosition
            => _iSeePlayer ? _player.position : _targets[_targetIndex].position;

        public Navigation(Action jump, Action attack, float attackRange, List<Transform> targets,
            float visualRange, LayerMask playerLayer)
        {
            _jump = jump;
            _targets = targets;
            _visualRange = visualRange;
            _playerLayer = playerLayer;
            _attack = attack;
            _attackRange = attackRange;
        }

        public void PlayerDetection(Vector2 position, bool isRightFacing)
        {
            if (_player == null)
            {
                Collider2D[] results = Physics2D.OverlapCircleAll(position, _visualRange, _playerLayer);

                if (results.Length == 0)
                    return;

                _player = results[0].transform;
            }
            else
            {
                if ((position - (Vector2)_player.position).magnitude > _visualRange)
                {
                    _iSeePlayer = false;
                    _player = null;
                    return;
                }
            }

            Vector2 directionToPlayer = ((Vector2)_player.position - position).normalized;
            Vector2 facingDirection = isRightFacing ? Vector2.left : Vector2.right;

            _iSeePlayer = Vector2.Dot(directionToPlayer, facingDirection) > 0;
          //  Debug.Log(_iSeePlayer ? $"Вижу игрока {(position - (Vector2)_player.position).magnitude} <= {_attackRange}" : "Игрока не вижу");

            if (_iSeePlayer && (position - (Vector2)_player.position).magnitude <= _attackRange)
                _attack();
        }

        public void TargetCheck(Vector2 position)
        {
            if ((GetTargetPosition - position).sqrMagnitude < 1 == false || _iSeePlayer)
                return;

            _targetIndex++;

            if (_targetIndex >= _targets.Count)
                _targetIndex = 0;
        }

        public void Jump(Vector2 position)
        {
            if (_iSeePlayer == false && (_lastPosition - position).sqrMagnitude < 0.0005f)
                _jump();
            _lastPosition = position;
        }
    }
}