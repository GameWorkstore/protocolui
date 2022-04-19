using System;
using GameWorkstore.Patterns;

namespace GameWorkstore.ProtocolUI
{
    public class UIStateService : IService
    {
        private readonly HighSpeedArray<RuntimeState> _states = new HighSpeedArray<RuntimeState>(100);
        private int _proc;
        private UIStateScriptable[] _procs;
        private int _layer;
		public const int NotSharedLayer = -1;

        public override void Preprocess()
        {
        }

        public override void Postprocess()
        {
        }
        
        public void RegisterState(UIStateScriptable state, int layer = NotSharedLayer)
        {
            _proc = state.Hash;
            int index = _states.IndexOf(IsEqual);
            if (index < 0)
            {
                _states.Add(new RuntimeState() { Scriptable = state, Layer = layer, Active = false });
            }
            else if (layer >= 0)
            {
                _states[index].Layer = layer;
            }
        }

        public void SetState(UIStateScriptable state, bool isActive = true)
        {
            _proc = state.Hash;
            int index = _states.IndexOf(IsEqual);
            if (index < 0)
            {
                _states.Add(new RuntimeState() { Scriptable = state, Layer = NotSharedLayer, Active = isActive });
                return;
            }
            _layer = _states[index].Layer;
            _states.ForEach(DisableIfNecessary);
            _states[index].Active = isActive;
        }

        public bool IsStateActive(UIStateScriptable state)
        {
            _proc = state.Hash;
            return _states.Any(IsActive);
        }

        public bool IsAnyStateActive(ref UIStateScriptable[] states)
        {
            _procs = states;
            return _states.Any(IsActiveMulty);
        }

        private void DisableIfNecessary(RuntimeState state)
        {
            state.Active &= state.Layer != _layer || state.Layer == NotSharedLayer;
        }

        private bool IsEqual(RuntimeState state)
        {
            return state.Scriptable.Hash == _proc;
        }

        private bool IsActive(RuntimeState state)
        {
            if (state.Scriptable.Hash == _proc) return state.Active;
            return false;
        }

        private bool IsActiveMulty(RuntimeState state)
        {
            for(int i = 0; i < _procs.Length; i++) if (state.Scriptable.Hash == _procs[i].Hash) return state.Active;
            return false;
        }
    }

    public class RuntimeState
    {
        public UIStateScriptable Scriptable;
        public bool Active;
        public int Layer;
    }
    
    [Serializable]
    public class StatePreview
    {
        public UIStateScriptable State;
        public int Layer;
    }
}
