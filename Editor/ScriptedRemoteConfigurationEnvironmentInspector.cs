using UnityEditor;
using UnityEngine;

namespace SpellBoundAR.RemoteConfiguration.Editor
{
    [CustomEditor(typeof(ScriptedRemoteConfigurationEnvironment), true)]
    public class ScriptedRemoteConfigurationEnvironmentInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Activate")) ((ScriptedRemoteConfigurationEnvironment) target).ActivateEnvironment();
        }
    }
}