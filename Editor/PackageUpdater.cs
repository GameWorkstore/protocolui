#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.PackageManager;

namespace GameWorkstore.ProtocolUI
{
    public static class PackageUpdater
    {
        [MenuItem("Help/PackageUpdate/GameWorkstore.ProtocolUI")]
        public static void TrackPackages()
        {
            Client.Add("git://github.com/GameWorkstore/protocolui.git");
        }
    }
}
#endif