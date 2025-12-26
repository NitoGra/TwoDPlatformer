using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal class Player : UnitHealth
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private Attacker _attackerAction;
        [SerializeField] private Camera _playerCamera;

        private JumpController _jumper;
        private UnitAnimationController _unitAnimationController;
        private InputSystemService _inputSystemService;
        private float _moveSpeed = 2f;
        private float _sprintSpeed = 2f;

        public void Init(float moveSpeed, float sprintSpeed, 
            float jumpForce, 
            int damage, int health,
            LayerMask groundLayer)
        {
            _sprintSpeed = sprintSpeed;
            _moveSpeed = moveSpeed;

            _attackerAction.Init(damage);
            _jumper = new(jumpForce, _rigidbody, groundLayer);
            _unitAnimationController = new(_animator);
            _inputSystemService = new(
                t => _jumper.Jump(transform.position),
                Sprint, SprintCanceled,
                t => _unitAnimationController.Attack());
            
            _playerCamera.gameObject.SetActive(true);
            base.Init(health, _unitAnimationController.Dead);
        }
        
        private void OnEnable() 
            => _inputSystemService.Enable();

        private void OnDisable() 
            => _inputSystemService.Disable();

        private void FixedUpdate() 
            => HandleMovement();
        
        private void Rotation(bool isRotateLeft) 
            => _animator.transform.eulerAngles = isRotateLeft ? new Vector3(0, 180, 0) : new Vector3(0, 0, 0);

        private void Sprint(InputAction.CallbackContext context) 
            => _moveSpeed += _sprintSpeed;

        private void SprintCanceled(InputAction.CallbackContext context) 
            => _moveSpeed -= _sprintSpeed;
        
        private void HandleMovement()
        {
            if (_inputSystemService.MoveInput == Vector2.zero)
            {
                _rigidbody.linearVelocity = new(_rigidbody.linearVelocity.x / 1.5f, _rigidbody.linearVelocity.y);
                _unitAnimationController.StopRun();
                return;
            }

            _unitAnimationController.StartRun();
            _rigidbody.linearVelocity =
                new((_inputSystemService.MoveInput * _moveSpeed).x, _rigidbody.linearVelocity.y);
            
            Rotation(_inputSystemService.MoveInput.x < 0);
        }
    }
}