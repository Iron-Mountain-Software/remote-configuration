using Unity.Services.RemoteConfig;
using UnityEngine;
using UnityEngine.Events;

namespace IronMountain.RemoteConfiguration
{
    [ExecuteAlways]
    public class RemoteSettingMonoBehaviour : MonoBehaviour, IRemoteSetting
    {
        [SerializeField] private string key;
        [SerializeField] private string defaultValue;
        [SerializeField] private string value;
        [Space]
        public UnityEvent<string> onValueChanged;
        
        public string Key => key;
        public string DefaultValue => defaultValue;

        public string Value
        {
            get => value;
            private set
            {
                if (this.value == value) return;
                this.value = value;
                onValueChanged?.Invoke(this.value);
            }
        }

        private void OnEnable()
        {
            RemoteConfigurationManager.RemoteSettings.Add(this);
            RemoteConfigurationManager.OnConfigurationReceived += OnConfigurationReceived;
            RefreshValue();
        }

        private void OnDisable()
        {
            RemoteConfigurationManager.RemoteSettings.Remove(this);
            RemoteConfigurationManager.OnConfigurationReceived -= OnConfigurationReceived;
        }
        
        private void OnConfigurationReceived(ConfigResponse configResponse)
        {
            RefreshValue();
        }

        private void RefreshValue()
        {
            Value = RemoteConfigService.Instance is {appConfig: { }} 
                ? RemoteConfigService.Instance.appConfig.GetString(key, defaultValue)
                : defaultValue;
        }
    }
}