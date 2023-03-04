using GameWorkstore.Patterns;
using UnityEngine;

namespace GameWorkstore.ProtocolUI
{
    public class UIAspectRatioService : IService
    {
        private int LastScreenWidth = -1;
        private int LastScreenHeight = -1;
        private AspectRatioConfig _config = null;

        public AspectRatioTarget CurrentAspectRatioTarget = AspectRatioTarget.A16X9;
        public Signal<AspectRatioTarget> OnAspectRatioChanged = new Signal<AspectRatioTarget>();

        public override void Preprocess()
        {
            ServiceProvider.GetService<EventService>().LateUpdate.Register(Update);
        }

        public override void Postprocess()
        {
            ServiceProvider.GetService<EventService>().LateUpdate.Unregister(Update);
        }

        public void Initialize(AspectRatioConfig config)
        {
            _config = config;
        }

        private void Update()
        {
            if (_config == null) return;
            if (LastScreenWidth == Screen.width && LastScreenHeight == Screen.height) return;
            LastScreenWidth = Screen.width;
            LastScreenHeight = Screen.height;

            bool isPortrait = Screen.height > Screen.width;
            var ratio = isPortrait ?
                Screen.height / (float)Screen.width :
                Screen.width / (float)Screen.height;

            foreach (var group in _config.AspectRatioGroup)
            {
                if (group.IsInRange(ratio))
                {
                    CurrentAspectRatioTarget = group.Target;
                    OnAspectRatioChanged.Invoke(group.Target);
                    return;
                }
            }

            //default
            CurrentAspectRatioTarget = _config.Default;
            OnAspectRatioChanged.Invoke(_config.Default);
        }
    }
}
