using UnityEngine;
using GameWorkstore.Patterns;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;
using System.Linq;

namespace GameWorkstore.ProtocolUI
{
    public class UICanvas : MonoBehaviour
    {
        public StatePreview[] LayeredStates;
        public UIStateScriptable[] ActiveStates;
        public UIStateScriptable[] EditorActiveStates;

        private void Awake()
        {
            var _stateService = ServiceProvider.GetService<UIStateService>();
            for (int i = 0; i < LayeredStates.Length; i++)
            {
                for (var j = 0; j < LayeredStates[i].States.Length; j++)
                {
                    var state = LayeredStates[i].States[j];
                    _stateService.RegisterState(state, LayeredStates[i].Layer);
                }
            }
#if UNITY_EDITOR
            for (int i = 0; i < EditorActiveStates.Length; i++)
            {
                _stateService.SetState(EditorActiveStates[i], true);
            }
#else
            for(int i=0;i < ActiveStates.Length; i++)
            {
                _stateService.SetState(ActiveStates[i], true);
            }
#endif
        }
    }
}