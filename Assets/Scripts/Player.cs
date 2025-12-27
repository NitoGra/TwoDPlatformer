using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal class Player : MonoBehaviour, IDamaging
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private Attacker _attackerAction;
        [SerializeField] private Camera _playerCamera;

        private HealthModel _health;
        private JumpController _jumper;
        private InputSystemService _inputSystemService;
        private CharacterAnimationController _characterAnimationController;
        private float _moveSpeed = 2f;
        private float _sprintSpeed = 2f;

        public void Init(PlayerConfig playerConfig, Action<int, int> viewHealth, float deadTime)
        {
            _sprintSpeed = playerConfig.SprintSpeed;
            _moveSpeed = playerConfig.MoveSpeed;

            _attackerAction.Init(playerConfig.Damage);
            _jumper = new(playerConfig.JumpForce, _rigidbody, playerConfig.GroundLayer);
            _characterAnimationController = new(_animator);
            _inputSystemService = new(
                t => _jumper.Jump(transform.position),
                Sprint, SprintCanceled,
                t => _characterAnimationController.Attack());

            _playerCamera.gameObject.SetActive(true);
            _health = new(playerConfig.MaxHealth);
            _health.Changed += viewHealth;
            _health.Died += () => DelayedDeath(deadTime/_animator.speed);
            _health.Died += _characterAnimationController.SetDead;
        }

        private void OnEnable() =>
            _inputSystemService.Enable();

        private void OnDisable() =>
            _inputSystemService.Disable();

        private void FixedUpdate() =>
            HandleMovement();

        private void OnCollisionEnter2D(Collision2D other) =>
            ContactService.ContactCheck(other, _health);

        private void Rotation(bool isRotateLeft) =>
            _animator.transform.eulerAngles = isRotateLeft ? new Vector3(0, 180, 0) : new Vector3(0, 0, 0);

        private void Sprint(InputAction.CallbackContext context) =>
            _moveSpeed += _sprintSpeed;

        private void SprintCanceled(InputAction.CallbackContext context) =>
            _moveSpeed -= _sprintSpeed;

        private void DelayedDeath(float delay) =>
            Invoke(nameof(Death), delay);

        private void Death() =>
            gameObject.SetActive(false);

        public void TakeDamage(int damage) =>
            _health.TakeDamage(damage);

        private void HandleMovement()
        {
            if (_inputSystemService.MoveInput.x == 0)
            {
                _rigidbody.linearVelocity = new(_rigidbody.linearVelocity.x / 1.5f, _rigidbody.linearVelocity.y);
                _characterAnimationController.StopRun();
                return;
            }

            _characterAnimationController.StartRun();
            _rigidbody.linearVelocity =
                new((_inputSystemService.MoveInput * _moveSpeed).x, _rigidbody.linearVelocity.y);

            Rotation(_inputSystemService.MoveInput.x < 0);
        }
    }
}