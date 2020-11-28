using GameWorkstore.Patterns;
using UnityEngine;
using UnityEngine.UI;

namespace GameWorkstore.ProtocolUI
{
    public class UIDisableStateButtonListener : MonoBehaviour
    {
        public string State;
        private UIStateService _uistateservice;
        private int _stateHash;

        // Start is called before the first frame update
        private void Awake()
        {
            _stateHash = Animator.StringToHash(State);
            _uistateservice = ServiceProvider.GetService<UIStateService>();
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _uistateservice.SetState(_stateHash, false);
        }
    }
}