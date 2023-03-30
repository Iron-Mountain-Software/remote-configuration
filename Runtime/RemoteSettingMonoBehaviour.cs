using Unity.Services.RemoteConfig;
using UnityEngine;
using UnityEngine.Events;

namespace SpellBoundAR.RemoteConfiguration
{
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
                onValueChanged?.Invoke(this.value);
            }
        }
        
        private void OnConfigurationReceived(ConfigResponse configResponse)
        {
            Value = RemoteConfigService.Instance.appConfig.GetString(key, defaultValue);
        }

        private void OnEnable()
        {
            Value = defaultValue;
            RemoteConfigurationManager.RemoteSettings.Add(this);
            RemoteConfigurationManager.OnConfigurationReceived += OnConfigurationReceived;
            onValueChanged?.Invoke(Value);
        }

        private void OnDisable()
        {
            RemoteConfigurationManager.RemoteSettings.Remove(this);
            RemoteConfigurationManager.OnConfigurationReceived -= OnConfigurationReceived;
        }
    }
}