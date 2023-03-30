using System.Collections.Generic;
using UnityEngine;

namespace SpellBoundAR.RemoteConfiguration
{
    public class RemoteSettingsDebugger : MonoBehaviour
    {
        public List<IRemoteSetting> RemoteSettings = RemoteConfigurationManager.RemoteSettings;
    }
}
