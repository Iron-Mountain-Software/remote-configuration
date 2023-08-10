using UnityEngine;
using UnityEngine.UI;

namespace SpellBoundAR.RemoteConfiguration
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
            Refresh();
        }

        private void OnDisable()
        {
            RemoteConfigurationManager.OnEnvironmentChanged -= Refresh;
        }

        private void Refresh()
        {
            if (!_text) return;
            _text.text = RemoteConfigurationManager.EnvironmentID;
        }
    }
}