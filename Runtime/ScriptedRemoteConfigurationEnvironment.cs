using UnityEngine;

namespace SpellBoundAR.RemoteConfiguration
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Remote Config Environment")]
    public class ScriptedRemoteConfigurationEnvironment : ScriptableObject
    {
        [SerializeField] private string environmentID;

        public string EnvironmentID => environmentID;

        public void ActivateEnvironment()
        {
            RemoteConfigurationManager.EnvironmentID = environmentID;
            RemoteConfigurationManager.FetchConfigs();
        }
    }
}