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
        [FormerlySerializedAs("_uiPanelComponentsParent")]
        public Transform CanvasRoot;
        public bool DebugEnabled;

        private void Awake()
        {
            if(CanvasRoot != null)
            {
                foreach(var panel in CanvasRoot.GetComponentsInChildren<UIPanel>(true))
                {
                    if (DebugEnabled) Debug.Log($"Panel {panel.name} registered");
                    panel.Register();
                }
            }
            else
            {
                //find all UI Panels on scene
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    foreach (var panels in SceneManager.GetSceneAt(i).GetRootGameObjects().Select(t => t.GetComponentsInChildren<UIPanel>(true)))
                    {
                        foreach (var panel in panels)
                        {
                            if (DebugEnabled) Debug.Log($"Panel {panel.name} registered");
                            panel.Register();
                        }
                    }
                }
            }

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