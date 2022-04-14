using System;
using GameWorkstore.Patterns;

namespace GameWorkstore.ProtocolUI
{
    public class UIStateService : IService
    {
        private readonly HighSpeedArray<State> _states = new HighSpeedArray<State>(100);
        private int _proc;
        private UIStateScriptable[] _procs;
        private int _layer;

        public override void Preprocess()
        {
        }

        public override void Postprocess()
        {
        }
        
        public void RegisterState(UIStateScriptable state, int layer = -1)
        {
            _proc = state.Hash;
            int index = _states.IndexOf(IsEqual);
            if (index < 0)
            {
                _states.Add(new State() { Scriptable = state, Layer = layer, Active = false });
            }
            else if (layer >= 0)
            {
                _states[index].Layer = layer;
            }
        }

        public void SetState(UIStateScriptable state, bool isActive)
        {
            _proc = state.Hash;
            int index = _states.IndexOf(IsEqual);
            if (index < 0)
            {
                _states.Add(new State() { Scriptable = state, Layer = -1, Active = isActive });
                return;
            }
            _layer = _states[index].Layer;
            if(isActive) _states.ForEach(Disable);
            _states[index].Active = isActive;
        }

        public bool IsActive(UIStateScriptable state)
        {
            _proc = state.Hash;
            return _states.Any(IsActive);
        }

        public bool IsActive(ref UIStateScriptable[] states)
        {
            _procs = states;
            return _states.Any(IsActiveMulty);
        }

        private void Disable(State state)
        {
            if (state.Layer == _layer) state.Active = false;
        }

        private bool IsEqual(State state)
        {
            return state.Scriptable.Hash == _proc;
        }

        private bool IsActive(State state)
        {
            if (state.Scriptable.Hash == _proc) return state.Active;
            return false;
        }

        private bool IsActiveMulty(State state)
        {
            for(int i = 0; i < _procs.Length; i++) if (state.Scriptable.Hash == _procs[i].Hash) return state.Active;
            return false;
        }
    }

    public class State
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
