using GameWorkstore.Patterns;
using UnityEngine;
using UnityEngine.UI;

namespace GameWorkstore.ProtocolUI
{
    public class UIActiveStateButtonListener : MonoBehaviour
    {
        public UIStateScriptable State;
        private UIStateService _uistateservice;

        // Start is called before the first frame update
        private void Awake()
        {
            _uistateservice = ServiceProvider.GetService<UIStateService>();
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _uistateservice.SetState(State, true);
        }
    }
}