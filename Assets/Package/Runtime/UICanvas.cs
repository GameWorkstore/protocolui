using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameWorkstore.Patterns;

namespace GameWorkstore.ProtocolUI
{
    public class UICanvas : MonoBehaviour
    {
        public StatePreview[] LayeredStates;
        public string[] ActiveStates;
        public string[] EditorActiveStates;
        private readonly HighSpeedArray<UIPanel> _panels = new HighSpeedArray<UIPanel>(128);

        private void Awake()
        {
            for(int i = 0; i < SceneManager.sceneCount; i++)
            {
                foreach (var panels in SceneManager.GetSceneAt(i).GetRootGameObjects().Select(t => t.GetComponentsInChildren<UIPanel>(true)))
                {
                    foreach(var panel in panels)
                    {
                        _panels.Add(panel);
                        panel.Register();
                    }
                }
            }

            var _stateService = ServiceProvider.GetService<UIStateService>();
            for (int i = 0; i < LayeredStates.Length; i++)
            {
                int hash = Animator.StringToHash(LayeredStates[i].State);
                _stateService.RegisterState(hash, LayeredStates[i].Layer);
            }
#if UNITY_EDITOR
            for (int i = 0; i < EditorActiveStates.Length; i++)
            {
                int hash = Animator.StringToHash(EditorActiveStates[i]);
                _stateService.SetState(hash, true);
            }
#else
            for(int i=0;i < ActiveStates.Length; i++)
            {
                int hash = Animator.StringToHash(ActiveStates[i]);
                _stateService.SetState(hash, true);
            }
#endif
        }

        private void OnDestroy()
        {
            _panels.ForEach(t => t.Unregister());
        }
    }
}