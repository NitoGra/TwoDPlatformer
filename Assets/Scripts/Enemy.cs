using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    internal class Enemy : Unit
    {        
        [SerializeField] private Navigation _navigation;

        public void Init(float moveSpeed, float sprintSpeed, float jumpForce, 
            int health, List<Transform> targets, float visualRange, float attackRange ,int damage, LayerMask playerLayer)
        {
            base.Init(moveSpeed, sprintSpeed, jumpForce, health, damage);
            _navigation = new(Jump, Attack, attackRange, targets,visualRange, playerLayer);
        }
        
        protected override void HandleMovement()
        {
            Vector2 movement = (_navigation.GetTargetPosition - (Vector2)transform.position).normalized * MoveSpeed;
            Rigidbody.linearVelocity = new(movement.x, Rigidbody.linearVelocity.y);
            
            Animator.transform.eulerAngles = movement.x < 0 ?
                new Vector3(0, 180, 0) : 
                new Vector3(0, 0, 0);
            
            Animator.SetBool(Run, true);
            
            _navigation.Jump(transform.position);
            _navigation.TargetCheck(transform.position);
            _navigation.PlayerDetection(transform.position, movement.x < 0);
        }
        
        private void Attack()
        {
            Animator.SetBool(Run, false);
            Animator.SetTrigger(AttackName);
        }
    }
    
}