using System.Collections.Generic;
using System;
using UnityEngine;

namespace Scripts
{
    internal class Enemy : MonoBehaviour, IDamaging
    {
        [SerializeField] private EnemyContext _enemyContext;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private Attacker _attackerAction;

        private JumpController _jumper;
        private CharacterAnimationController _characterAnimationController;
        private float _moveSpeed = 2f;
        private HealthModel _health;
        
        private float MoveDirection => (_enemyContext.GetTargetPosition - (Vector2)transform.position).normalized.x;

        public void Init(EnemyConfig enemyConfig, List<Transform> patrolTargets,
            Action<int,int> viewHealth, float deadTime)
        {
            _moveSpeed = enemyConfig.MoveSpeed;

            _attackerAction.Init(enemyConfig.Damage);
            _jumper = new(enemyConfig.JumpForce, _rigidbody, enemyConfig.GroundLayer);
            _characterAnimationController = new(_animator);

            _enemyContext = new(patrolTargets,enemyConfig, transform);
            
            _health = new(enemyConfig.MaxHealth);
            _health.Changed += viewHealth;
            _health.Died += () => DelayedDeath(deadTime);
            _health.Died += _characterAnimationController.SetDead;
        }
        
        private void FixedUpdate()
        {
            HandleMovement();

            _enemyContext.TargetCheck();
            _enemyContext.DetectPlayer(MoveDirection < 0);

            if (_enemyContext.CanJump())
                _jumper.Jump(transform.position);
            else if (_enemyContext.CanAttack)
                Attack();
        }
        
        private void OnCollisionEnter2D(Collision2D other) =>
            ContactService.ContactCheck(other, _health);

        private void Rotation(bool isRotateLeft) =>
            _animator.transform.eulerAngles = isRotateLeft ? new Vector3(0, 180, 0) : new Vector3(0, 0, 0);

        private void HandleMovement()
        {
            _rigidbody.linearVelocity = new(MoveDirection * _moveSpeed, _rigidbody.linearVelocity.y);
            Rotation(MoveDirection < 0);
            _characterAnimationController.StartRun();
        }

        private void Attack()
        {
            _characterAnimationController.StopRun();
            _characterAnimationController.Attack();
        }

        private void DelayedDeath(float delay) => 
            Invoke(nameof(Death), delay);

        private void Death() => 
            gameObject.SetActive(false);

        public void TakeDamage(int damage) =>
            _health.TakeDamage(damage);
    }
}