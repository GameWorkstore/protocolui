using UnityEngine;

namespace GameWorkstore.ProtocolUI
{
    [CreateAssetMenu(fileName=nameof(UIStateScriptable),menuName="ProtocolUI/"+nameof(UIStateScriptable))]
    public class UIStateScriptable : ScriptableObject
    {
        private bool _IsInitialized = false;
        private int _cache = -1;

        public int Hash
        {
            get
            {
                if (_IsInitialized) return _cache;
                _cache = Animator.StringToHash(name);
                _IsInitialized = true;
                return _cache;
            }
        }
    }
}