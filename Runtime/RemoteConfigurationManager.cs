using System;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using UnityEngine;

namespace SpellBoundAR.RemoteConfiguration
{
    public static class RemoteConfigurationManager
    {
        public static event Action OnInitialized;
        public static event Action OnEnvironmentChanged;
        public static event Action<ConfigResponse> OnConfigurationReceived;

        public static readonly List<IRemoteSetting> RemoteSettings = new ();

        public static string EnvironmentID
        {
            get
            {
                if (RemoteConfigService.Instance == null) return string.Empty;
                if (RemoteConfigService.Instance.appConfig == null) return string.Empty;
                return RemoteConfigService.Instance.appConfig.environmentId;
            }
            set
            {
                if (RemoteConfigService.Instance == null) return;
                RemoteConfigService.Instance.SetEnvironmentID(value);
                OnEnvironmentChanged?.Invoke();
            }
        }

        public static bool Initialized { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void StartInitialization()
        {
            if (Initialized) return;
            DoInitialization();
        }

        private static async void DoInitialization()
        {
            await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
            RemoteConfigService.Instance.FetchCompleted += OnFetchCompleted;
            FetchConfigs();
            Initialized = true;
            OnInitialized?.Invoke();
        }

        public static void FetchConfigs()
        {
            if (!Initialized || RemoteConfigService.Instance == null) return;
            RemoteConfigService.Instance.FetchConfigs(new UserAttributes(), new AppAttributes());
        }

        private static void OnFetchCompleted(ConfigResponse response)
        {
            OnConfigurationReceived?.Invoke(response);
        }
    }
}