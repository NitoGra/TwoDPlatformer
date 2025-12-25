using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    internal class Enemy : UnitHealth
    {
        [SerializeField] private Navigation _navigation;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private Attacker _attackerAction;

        private JumpController _jumper;
        private AnimationController _animationController;
        private float _moveSpeed = 2f;

        public void Init(float moveSpeed, float jumpForce,
            float visualRange, float attackRange,
            int damage, int health,
            LayerMask playerLayer, LayerMask groundLayer,
            List<Transform> patrolTargets)
        {
            _moveSpeed = moveSpeed;

            _attackerAction.Init(damage);
            _jumper = new(jumpForce, _rigidbody, groundLayer);
            _animationController = new(_animator);

            base.Init(health, _animationController.Dead);
            _navigation = new(
                attackRange, 
                patrolTargets, 
                visualRange, 
                playerLayer);
        }
        
        private void FixedUpdate()
            => HandleMovement();

        private void Rotation(bool isRotateLeft) 
            => _animator.transform.eulerAngles = isRotateLeft ? new Vector3(0, 180, 0) : new Vector3(0, 0, 0);

        private void HandleMovement()
        {
            Vector2 myPosition = transform.position;
            Vector2 movement = (_navigation.GetTargetPosition - myPosition).normalized * _moveSpeed;
            
            _rigidbody.linearVelocity = new(movement.x, _rigidbody.linearVelocity.y);

            Rotation(movement.x < 0);
            _animationController.StartRun();
            
            if (_navigation.DoINeedJump(myPosition))
                _jumper.Jump(myPosition);

            if (_navigation.DoYouNeedAttack(myPosition, movement.x < 0))
                Attack();
        }

        private void Attack()
        {
            _animationController.StopRun();
            _animationController.Attack();
        }
    }
}