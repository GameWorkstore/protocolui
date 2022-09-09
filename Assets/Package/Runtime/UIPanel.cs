using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using GameWorkstore.Patterns;

namespace GameWorkstore.ProtocolUI
{
    public class UIPanel : MonoBehaviour
    {
        public UIStateScriptable[] ActiveStates;
        public Selectable FirstSelected;
        public bool ShowInstantly = true;
        public bool HideInstantly = true;

        public static int FrameUpdate = 10;
        private UIStateService _stateService;
        private bool _initialized = false;
        private int _frameCount = 0;
        private bool _isShowing = false;

        public virtual void Register()
        {
            _stateService = ServiceProvider.GetService<UIStateService>();
            for (int i = 0; i < ActiveStates.Length; i++)
            {
                _stateService.RegisterState(ActiveStates[i]);
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

            bool isPanelActive = _stateService.IsAnyStateActive(ref ActiveStates);

            if (gameObject.activeSelf != isPanelActive || !_initialized && isPanelActive)
            {
                _initialized = true;
                if (isPanelActive)
                {
                    if (!_isShowing)
                    {
                        _isShowing = true;
                        
                        if (ShowInstantly)
                        {
                            CompleteShow();
                        }
                        else
                        {
                            PrepareToShow(); 
                        }
                    }
                }
                else
                {
                    if (_isShowing)
                    {
                        _isShowing = false;
                        if (HideInstantly)
                        {
                            CompleteHide();
                        }
                        else
                        {
                            PrepareToHide();
                        }
                    }
                }

                if (isPanelActive && FirstSelected != null)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(FirstSelected.gameObject);
                }
            }
        }

        /// <summary>
        /// The framework requests to the panel to become visible.
        /// </summary>
        public virtual void PrepareToShow() { }

        /// <summary>
        /// The framework requests to the panel to become invisible.
        /// </summary>
        public virtual void PrepareToHide() { }

        protected void CompleteShow()
        {
            gameObject.SetActive(true);
        }

        protected void CompleteHide()
        {
            gameObject.SetActive(false);
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