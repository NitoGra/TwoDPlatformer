using UnityEngine;

namespace Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal class Player : Unit
    {
        private InputSystem _inputSystem;   

        public override void Init(float moveSpeed, float sprintSpeed, float jumpForce, int health, int damage)
        {
            _inputSystem = new(t => Jump(), Sprint, SprintCanceled, t => Attack());
            base.Init(moveSpeed, sprintSpeed, jumpForce, health, damage);
        }

        private void OnEnable()
        {
            _inputSystem.Enable();
        }

        private void OnDisable()
        {
            _inputSystem.Disable();
        }

        protected override void HandleMovement()
        {
            if (_inputSystem.MoveInput == Vector2.zero)
            {
                Animator.SetBool(Run, false);
                Rigidbody.linearVelocity = new(Rigidbody.linearVelocity.x/1.5f, Rigidbody.linearVelocity.y);
                return;
            }
            
            Animator.SetBool(Run, true);
            Vector2 movement = _inputSystem.MoveInput * MoveSpeed;
            Animator.transform.eulerAngles = movement.x < 0 ? new Vector3(0, 180, 0) : new Vector3(0, 0, 0);
            Rigidbody.linearVelocity = new(movement.x, Rigidbody.linearVelocity.y);
        }
        
        private void Attack()
        {
            Animator.SetTrigger(AttackName);
        }
    }
}