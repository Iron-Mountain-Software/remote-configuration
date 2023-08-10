using UnityEditor;
using UnityEngine;

namespace SpellBoundAR.RemoteConfiguration.Editor
{
    [CustomEditor(typeof(RemoteSettingsDebugger))]
    public class RemoteConfigurationDebugWindow : UnityEditor.Editor
    {
        private string _environmentID;

        public override void OnInspectorGUI()
        {
            _environmentID = GUILayout.TextField(_environmentID);
            if (GUILayout.Button("Set Environment ID"))
            {
                RemoteConfigurationManager.EnvironmentID = _environmentID;
            }
            if (GUILayout.Button("Fetch Configs"))
            {
                RemoteConfigurationManager.FetchConfigs();
            }
            foreach (IRemoteSetting setting in RemoteConfigurationManager.RemoteSettings)
            {
                if (setting == null) continue;
                GUILayout.BeginHorizontal();
                GUILayout.Label(setting.Key);
                GUILayout.Label(setting.Value);
                GUILayout.EndHorizontal();
            }
        }
    }
}