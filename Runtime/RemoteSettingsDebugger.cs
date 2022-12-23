using System.Collections.Generic;
using UnityEngine;

namespace SpellBoundAR.RemoteConfiguration
{
    public class RemoteSettingsDebugger : MonoBehaviour
    {
        public List<RemoteSetting> remoteSettings = RemoteConfigurationManager.RemoteSettings;
    }
}
