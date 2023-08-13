using Unity.Services.RemoteConfig;
using UnityEngine;
using UnityEngine.UI;

namespace IronMountain.RemoteConfiguration
{
    [ExecuteAlways]
    [RequireComponent(typeof(Text))]
    public class EnvironmentIDText : MonoBehaviour
    {
        [Header("Cache")]
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        private void OnEnable()
        {
            RemoteConfigurationManager.OnEnvironmentChanged += Refresh;
            if (RemoteConfigService.Instance != null)
            {
                RemoteConfigService.Instance.FetchCompleted += OnFetchCompleted;
            }
            Refresh();
        }

        private void OnDisable()
        {
            RemoteConfigurationManager.OnEnvironmentChanged -= Refresh;
            if (RemoteConfigService.Instance != null)
            {
                RemoteConfigService.Instance.FetchCompleted -= OnFetchCompleted;
            }
        }
        
        private void OnFetchCompleted(ConfigResponse response)
        {
            Refresh();
        }

        private void Refresh()
        {
            if (!_text) return;
            _text.text = RemoteConfigurationManager.EnvironmentID;
        }
    }
}