using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu(fileName = "AnimationClipSettings", menuName = "Game2D/AnimationClips")]
    internal class AnimationClipSettings : ScriptableObject
    {        
        [SerializeField] private AnimationClip _deadClip;
        
        public float GetDeadClipLength => _deadClip.length;
    }
}