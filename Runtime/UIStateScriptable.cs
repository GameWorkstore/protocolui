using UnityEngine;

namespace GameWorkstore.ProtocolUI
{
    [CreateAssetMenu(fileName = nameof(UIStateScriptable), menuName = "ProtocolUI/" + nameof(UIStateScriptable))]
    public class UIStateScriptable : ScriptableObject
    {
#if !UNITY_EDITOR
        private bool _IsInitialized = false;
        private int _cache = -1;
#endif

        public int Hash
        {
            get
            {
#if UNITY_EDITOR
                return Animator.StringToHash(name);
#else
                if (_IsInitialized) return _cache;
                _cache = Animator.StringToHash(name);
                _IsInitialized = true;
                return _cache;
#endif
            }
        }
    }
}