using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts
{
    internal class InputSystemService
    {
        private readonly InputSystem_Actions _inputs;
        private readonly Action<InputAction.CallbackContext> _jump;
        private readonly Action<InputAction.CallbackContext> _attack;
        private readonly Action<InputAction.CallbackContext> _sprint;
        private readonly Action<InputAction.CallbackContext> _onSprintCanceled;

        public InputSystemService(Action<InputAction.CallbackContext> jump,
                           Action<InputAction.CallbackContext> sprint,
                           Action<InputAction.CallbackContext> onSprintCanceled,
                           Action<InputAction.CallbackContext> attack)
        {
            _jump = jump;
            _sprint = sprint;
            _attack = attack;
            _onSprintCanceled = onSprintCanceled;
            
            _inputs = new InputSystem_Actions();
            _inputs.Player.Enable();
        }

        public Vector2 Look { get; private set; }
        public Vector2 MoveInput { get; private set; }

        public void Enable()
        {
            SubscribeInput(_inputs.Player.Move, OnMove, OnMoveCanceled);
            SubscribeInput(_inputs.Player.Look, OnMouseMove, OnMouseCanceled);
            SubscribeInput(_inputs.Player.Sprint, _sprint, _onSprintCanceled);
            SubscribeInput(_inputs.Player.Attack, _attack);
            SubscribeInput(_inputs.Player.Jump, _jump);
        }

        public void Disable()
        {
            UnsubscribeInput(_inputs.Player.Move, OnMove, OnMoveCanceled);
            UnsubscribeInput(_inputs.Player.Look, OnMouseMove, OnMouseCanceled);
            UnsubscribeInput(_inputs.Player.Sprint, _sprint, _onSprintCanceled);
            UnsubscribeInput(_inputs.Player.Attack, _attack);
            UnsubscribeInput(_inputs.Player.Jump, _jump);
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            MoveInput = Vector2.zero;
        }

        private void OnMouseMove(InputAction.CallbackContext context)
        {
            Look = context.ReadValue<Vector2>();
        }

        private void OnMouseCanceled(InputAction.CallbackContext context)
        {
            Look = Vector2.zero;
        }

        private void SubscribeInput(InputAction input,
            Action<InputAction.CallbackContext> performed = null,
            Action<InputAction.CallbackContext> canceled = null)
        {
            if (performed != null)
                input.performed += performed;

            if (canceled != null)
                input.canceled += canceled;
        }

        private void UnsubscribeInput(InputAction input,
            Action<InputAction.CallbackContext> performed = null,
            Action<InputAction.CallbackContext> canceled = null)
        {
            if (performed != null)
                input.performed -= performed;

            if (canceled != null)
                input.canceled -= canceled;
        }
    }
}