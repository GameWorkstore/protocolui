using System;
using UnityEngine;

namespace GameWorkstore.ProtocolUI
{
    public enum AspectRatioTarget
    {
        A4X3,
        A16X9
    }

    [Serializable]
    public struct AspectRatioGroup
    {
        public AspectRatioTarget Target;
        public float Min;
        public float Max;

        public bool IsInRange(float value) => value >= Min && value < Max;
    }

    [CreateAssetMenu(fileName=nameof(AspectRatioConfig),menuName="ProtocolUI/"+nameof(AspectRatioConfig))]
    public class AspectRatioConfig : ScriptableObject
    {
        public AspectRatioGroup[] AspectRatioGroup = new AspectRatioGroup[]
        {
            new AspectRatioGroup()
            {
                Min = 1,
                Max = 1.45f,
                Target = AspectRatioTarget.A4X3
            },
            new AspectRatioGroup()
            {
                Min = 1.45f,
                Max = 100000,
                Target = AspectRatioTarget.A16X9
            }
        };
        public AspectRatioTarget Default = AspectRatioTarget.A16X9;
    }
}
