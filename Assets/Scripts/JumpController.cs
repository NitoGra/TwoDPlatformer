using System;
using UnityEngine;

namespace Scripts
{
    [Serializable]
    internal class JumpController
    {
        private const float GroundCheckDistance = 1.2f;
        private float _jumpForce;
        private Rigidbody2D _rigidbody;
        private LayerMask _groundLayer;
        
        public JumpController(float jumpForce, Rigidbody2D rigidbody, LayerMask groundLayer)
        {
            _jumpForce = jumpForce;
            _rigidbody = rigidbody;
            _groundLayer = groundLayer;
        }
        
        public void Jump(Vector2 jumper)
        {
            Debug.DrawRay(jumper, Vector2.down * GroundCheckDistance, Color.red,GroundCheckDistance);

            if (Physics2D.Raycast(jumper, Vector2.down, GroundCheckDistance, _groundLayer) == false)
                return;

            _rigidbody.linearVelocity += _jumpForce * Vector2.up;
        }
    }
}