using GameWorkstore.Patterns;
using UnityEngine;

namespace GameWorkstore.ProtocolUI
{
    public class UIAspectRatioSelector : MonoBehaviour
    {
        public GameObject A16x9;
        public GameObject A4x3;

        public void OnEnable()
        {
            var aspectRatio = ServiceProvider.GetService<UIAspectRatioService>();
            aspectRatio.OnAspectRatioChanged.Register(OnAspectRatioChanged);
            OnAspectRatioChanged(aspectRatio.CurrentAspectRatioTarget);
        }

        private void OnAspectRatioChanged(AspectRatioTarget current)
        {
            A16x9.SetActive(current == AspectRatioTarget.A16X9);
            A4x3.SetActive(current == AspectRatioTarget.A4X3);
        }
    }
}
