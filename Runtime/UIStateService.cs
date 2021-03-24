using System;
using GameWorkstore.Patterns;

namespace GameWorkstore.ProtocolUI
{
    public class UIStateService : IService
    {
        private readonly HighSpeedArray<State> _states = new HighSpeedArray<State>(100);
        private int _proc;
        private int[] _procs;
        private int _layer;

        public override void Preprocess()
        {
        }

        public override void Postprocess()
        {
        }
        
        public void RegisterState(int hash, int layer = -1)
        {
            _proc = hash;
            int index = _states.IndexOf(IsEqual);
            if (index < 0)
            {
                _states.Add(new State() { Hash = hash, Layer = layer, Active = false });
            }
            else if (layer >= 0)
            {
                _states[index].Layer = layer;
            }
        }

        public void SetState(int hash, bool isActive)
        {
            _proc = hash;
            int index = _states.IndexOf(IsEqual);
            if (index < 0)
            {
                _states.Add(new State() { Hash = hash, Layer = -1, Active = isActive });
                return;
            }
            _layer = _states[index].Layer;
            if(isActive) _states.ForEach(Disable);
            _states[index].Active = isActive;
        }

        public bool IsActive(int activeStatesHash)
        {
            _proc = activeStatesHash;
            return _states.Any(IsActive);
        }

        public bool IsActive(ref int[] activeStatesHashs)
        {
            _procs = activeStatesHashs;
            return _states.Any(IsActiveMulty);
        }

        private void Disable(State state)
        {
            if (state.Layer == _layer) state.Active = false;
        }

        private bool IsEqual(State state)
        {
            return state.Hash == _proc;
        }

        private bool IsActive(State state)
        {
            if (state.Hash == _proc) return state.Active;
            return false;
        }

        private bool IsActiveMulty(State state)
        {
            for(int i = 0; i < _procs.Length; i++) if (state.Hash == _procs[i]) return state.Active;
            return false;
        }
    }

    public class State
    {
        public int Hash;
        public bool Active;
        public int Layer;
    }
    
    [Serializable]
    public class StatePreview
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2235:Mark all non-serializable fields", Justification = "Non issue.")]
        public string State;
        public int Layer;
    }
}
