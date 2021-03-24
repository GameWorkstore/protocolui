using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using GameWorkstore.Patterns;

namespace GameWorkstore.ProtocolUI
{
    public class UIPanel : MonoBehaviour
    {
        public string[] ActiveStates;
        public Selectable FirstSelected;

        private const int FrameUpdate = 10;
        private UIStateService _stateService;
        private bool _initialized = false;
        private int _frameCount = 0;
        private int[] _activeStatesHash;

        public virtual void Register()
        {
            _activeStatesHash = new int[ActiveStates.Length];
            _stateService = ServiceProvider.GetService<UIStateService>();
            for (int i = 0; i < _activeStatesHash.Length; i++)
            {
                _activeStatesHash[i] = Animator.StringToHash(ActiveStates[i]);
                _stateService.RegisterState(_activeStatesHash[i]);
            }
            ServiceProvider.GetService<EventService>().Update.Register(UpdatePanel);
        }

        public virtual void Unregister()
        {
            ServiceProvider.GetService<EventService>().Update.Unregister(UpdatePanel);
        }

        public void UpdatePanel()
        {
            if (_frameCount++ % FrameUpdate > 0) return;

            bool isActive = _stateService.IsActive(ref _activeStatesHash);

            if (gameObject.activeSelf != isActive || !_initialized && isActive)
            {
                _initialized = true;
                gameObject.SetActive(isActive);
                if (isActive && FirstSelected != null)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(FirstSelected.gameObject);
                }
            }
        }

        public void Select(GameObject gameObject)
        {
            if (gameObject == null) return;

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(gameObject);
        }

        public void Select(Selectable selectable)
        {
            if (selectable == null) return;

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(selectable.gameObject);
        }

        public void ClearSelection()
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}