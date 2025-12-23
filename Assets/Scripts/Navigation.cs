using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    [Serializable]
    internal class Navigation
    {
        private readonly Action _jump;
        
        private List<Transform> _targets;
        private int _targetIndex = 0;
        private Vector2 _lastPosition;

        public Vector2 GetTargetPosition
        {
            get { return _targets[_targetIndex].position; }
        }

        public Navigation(Action jump, List<Transform> targets)
        {
            _jump = jump;
            _targets = targets;
        }

        public void TargetCheck(Vector2 position)
        {
            if ((GetTargetPosition - position).sqrMagnitude < 1 == false)
                return;

            _targetIndex++;

            if (_targetIndex >= _targets.Count)
                _targetIndex = 0;
        }

        public void Jump(Vector2 position)
        {
            if (_lastPosition == position)
                _jump();
            _lastPosition = position;
        }
    }
}