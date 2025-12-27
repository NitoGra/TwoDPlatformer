using System;
using UnityEngine;

namespace Scripts
{
    [Serializable]
    internal class CharacterAnimationController
    {
        protected readonly int RunName = Animator.StringToHash("Run");
        protected readonly int AttackName = Animator.StringToHash("Attack");
        protected readonly int DeadName = Animator.StringToHash("Dead");
        
        private Animator _animator;
    
        public CharacterAnimationController(Animator animator)
        {
            _animator = animator;
        }
        
        public void StartRun()
        {
            _animator.SetBool(RunName, true);
        }
        
        public void StopRun()
        {
            _animator.SetBool(RunName, false);
        }
        
        public void SetDead()
        {
            _animator.SetTrigger(DeadName);
        }
        
        public void Attack()
        {
            _animator.SetTrigger(AttackName);
        }
    }
}