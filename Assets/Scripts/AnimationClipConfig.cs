using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu(fileName = "AnimationClipConfig", menuName = "Game2D/AnimationClips")]
    internal class AnimationClipConfig : ScriptableObject
    {        
        [SerializeField] private AnimationClip _deadClip;
        
        public float GetDeadClipLength => _deadClip.length;
    }
}