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
            SetEnvironmentID();
            RemoteConfigService.Instance.FetchCompleted += FetchCompleted;
            RemoteConfigService.Instance.FetchConfigs(new UserAttributes(), new AppAttributes());
            Initialized = true;
        }

        private static void SetEnvironmentID()
        {
            ScriptedAppReleaseVariant currentAppReleaseVariant = (ScriptedAppReleaseVariant) AppReleaseVariantsManager.GetCurrentAppReleaseVariant();
            if (currentAppReleaseVariant == null) return;
            RemoteConfigService.Instance.SetEnvironmentID(currentAppReleaseVariant.RemoteConfigEnvironmentID);
        }
        
        private static void FetchCompleted(ConfigResponse response)
        {
            OnConfigurationReceived?.Invoke(response);
        }
    }
}