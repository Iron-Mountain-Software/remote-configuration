using UnityEditor;
using UnityEngine;

namespace IronMountain.RemoteConfiguration.Editor
{
    [CustomEditor(typeof(RemoteConfigurationManager))]
    public class RemoteConfigurationManagerInspector : UnityEditor.Editor
    {
        private ScriptedRemoteConfigurationEnvironment _environment;
        
        public override void OnInspectorGUI()
        {
            DrawObjectFields();
            GUILayout.Space(10);
            DrawEditorFields();
            GUILayout.Space(10);
            DrawRemoteSettingsList();
        }

        private void DrawObjectFields()
        {
            SerializedProperty initializeEnvironmentOnAwake =
                serializedObject.FindProperty("initializeEnvironmentOnAwake");
            EditorGUILayout.PropertyField(initializeEnvironmentOnAwake);
            if (initializeEnvironmentOnAwake.boolValue)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("initializationEnvironment"));
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawEditorFields()
        {
            GUILayout.Label("Editor use only:");
            _environment = EditorGUILayout.ObjectField(
                "Environment to activate",
                _environment, 
                typeof(ScriptedRemoteConfigurationEnvironment),
                false) as ScriptedRemoteConfigurationEnvironment;
            if (GUILayout.Button("Activate Environment") && _environment)
            {
                _environment.ActivateEnvironment();
            }
        }

        private void DrawRemoteSettingsList()
        {
            GUILayout.Label("Remote Settings:");
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