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
        public static event Action<ConfigResponse> OnConfigurationReceived;

        public static readonly List<IRemoteSetting> RemoteSettings = new ();
        
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

        public static void SetEnvironmentID(string environmentID)
        {
            if (RemoteConfigService.Instance == null) return;
            RemoteConfigService.Instance.SetEnvironmentID(environmentID);
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