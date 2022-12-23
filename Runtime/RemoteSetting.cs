using System;
using Unity.Services.RemoteConfig;
using UnityEngine;

namespace SpellBoundAR.RemoteConfiguration
{
    [Serializable]
    public class RemoteSetting
    {
        public event Action OnValueChanged;
    
        [SerializeField] private string key;
        [SerializeField] private string defaultValue;
        [SerializeField] private string value;
        
        public string Value
        {
            get
            {
                Value = RemoteConfigurationManager.Initialized
                    ? RemoteConfigService.Instance.appConfig.GetString(key, defaultValue)
                    : defaultValue;
                return value;
            }
            private set
            {
                if (this.value == value) return;
                this.value = value;
                OnValueChanged?.Invoke();
            }
        }
        
        private void OnConfigurationReceived(ConfigResponse configResponse)
        {
            Value = RemoteConfigService.Instance.appConfig.GetString(key, defaultValue);
        }
        
        public RemoteSetting(string key, string defaultValue)
        {
            this.key = key;
            this.defaultValue = defaultValue;
            Value = defaultValue;
            RemoteConfigurationManager.RemoteSettings.Add(this);
            RemoteConfigurationManager.OnConfigurationReceived += OnConfigurationReceived;
        }

        ~RemoteSetting()
        {
            RemoteConfigurationManager.RemoteSettings.Remove(this);
            RemoteConfigurationManager.OnConfigurationReceived -= OnConfigurationReceived;
        }
    }
}
