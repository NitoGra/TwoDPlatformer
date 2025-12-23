using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts
{
    [Serializable]
    internal abstract class Unit : MonoBehaviour, IDamageable, IHealable
    {
        protected readonly int Run = Animator.StringToHash("Run");
        protected readonly int AttackName = Animator.StringToHash("Attack");
        private const float GroundCheckDistance = 1.2f;

        [SerializeField] protected Animator Animator;
        [SerializeField] protected Rigidbody2D Rigidbody;
        
        [SerializeField] private Attack _attackAction;
        [SerializeField] private LayerMask _groundLayer;

        protected float MoveSpeed = 4f;
        
        private float _sprintSpeed = 2f;
        private float _jumpForce = 4f;
        private Health _health;

        public virtual void Instantiate(float moveSpeed, float sprintSpeed, float jumpForce, int health, int damage)
        {
            _health = new(health);
            _health.OnDeath += Dead;
            
            MoveSpeed = moveSpeed;
            _sprintSpeed = sprintSpeed;
            _jumpForce = jumpForce;
            
            _attackAction.Instantiate(damage);
        }
        
        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }
        
        public void FixedUpdate()
        {
            HandleMovement();
        }
        
        protected abstract void HandleMovement();

        protected void Jump()
        {
            Debug.DrawRay((Vector2)transform.position, Vector2.down * GroundCheckDistance, Color.red,GroundCheckDistance);
            
            Animator.SetBool(Run, false);
            if (Physics2D.Raycast(transform.position, Vector2.down, GroundCheckDistance, _groundLayer) == false)
                return;

            Rigidbody.linearVelocity += _jumpForce * Vector2.up;
        }

        protected void Sprint(InputAction.CallbackContext context)
        {
            MoveSpeed += _sprintSpeed;
        }

        protected void SprintCanceled(InputAction.CallbackContext context)
        {
            MoveSpeed -= _sprintSpeed;
        }
        
        private void Dead()
        {
            this.enabled = false;
            Animator.enabled = false;
        }

        public bool TryHeal(int heal)
        {
            if (_health.TempHealth >= _health.MaxHealth)
                return false;
            
            _health.Heal(heal);
            return true;
        }
    }
}