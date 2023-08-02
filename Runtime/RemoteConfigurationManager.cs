using System;
using System.Collections.Generic;
using SpellBoundAR.AppIdentification;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using UnityEngine;

namespace SpellBoundAR.RemoteConfiguration
{
    public static class RemoteConfigurationManager
    {
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
            SetEnvironmentID();
            FetchConfigs();
            Initialized = true;
        }

        private static void SetEnvironmentID()
        {
            ScriptedAppReleaseVariant currentAppReleaseVariant = (ScriptedAppReleaseVariant) AppReleaseVariantsManager.GetCurrentAppReleaseVariant();
            if (currentAppReleaseVariant == null) return;
            SetEnvironmentID(currentAppReleaseVariant.RemoteConfigEnvironmentID);
        }
        
        public static void SetEnvironmentID(string environmentID)
        {
            Debug.Log("Setting Environment ID: " + environmentID);
            if (RemoteConfigService.Instance == null) return;
            RemoteConfigService.Instance.SetEnvironmentID(environmentID);
        }

        public static void FetchConfigs()
        {
            Debug.Log("Fetching Configs");
            if (RemoteConfigService.Instance == null) return;
            RemoteConfigService.Instance.FetchConfigs(new UserAttributes(), new AppAttributes());
        }

        private static void OnFetchCompleted(ConfigResponse response)
        {
            Debug.Log("Fetch Completed");
            OnConfigurationReceived?.Invoke(response);
        }
    }
}