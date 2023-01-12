using GameWorkstore.Patterns;
using UnityEngine;

namespace GameWorkstore.ProtocolUI
{
    public class UIAspectRatioInitializer : MonoBehaviour
    {
        public AspectRatioConfig Config;

        private void Awake()
        {
            ServiceProvider.GetService<UIAspectRatioService>().Initialize(Config);
        }
    }
}
