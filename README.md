# Remote Configuration
*Version: 1.1.4*
## Description: 
A library for interfacing with Unity's built in Remote Config service.

SOME CODING REQUIRED

Unity's Remote Config service is an awesome solution that I've used across multiple projects, but it requires some boilerplate code to get up and running. This package contains those scripts that I use to make it work.
## Use Cases: 
* Quickly standing up Unity's Remote Config service.  
* An improved system for switching environments at runtime. 
* An improved system for handling and using remote config variables in scripts. 
## Dependencies: 
* com.unity.remote-config (3.3.2)
## Directions for Use: 
1. Place the RemoteConfigurationManager on a gameobject in your scene. This component is responsible for initializing the service and setting the environment. 
1. If you want the RemoteConfigurationManager to initialize the environment (you probably do): 
   1. Check the box for "Initialize Environment on Awake"
   1. Create a ScriptedRemoteConfigurationEnvironment instance (Create > Scriptable Objects > Remote Config Environment).
   1. Copy your environment ID for the remote config dashboard into this object. 
   1. Drag the ScriptedRemoteConfigurationEnvironment onto the RemoteConfigurationManager into the "Initialization Environment" property.
1. There are 2 new ways to access your remote configuration variables:
   1. Create an instance of RemoteSetting in a script:
RemoteSetting mySetting = new RemoteSetting("key", "default value");
string value = mySetting.Value;
mySetting.OnValueChanged += ValueChangedHandler;
   1. Add a RemoteSettingMonobehaviour onto a gameobject, and configure the key and defaultValue fields. In another script:
RemoteSettingMonobehaviour mySetting;
string value = mySetting.Value;
mySetting.OnValueChanged += ValueChangedHandler;
## Package Mirrors: 
[<img src='https://img.itch.zone/aW1nLzEzNzQ2ODg3LnBuZw==/original/npRUfq.png'>](https://github.com/Iron-Mountain-Software/remote-configuration)[<img src='https://img.itch.zone/aW1nLzEzNzQ2ODk4LnBuZw==/original/Rv4m96.png'>](https://github.com/Iron-Mountain-Software/remote-configuration)[<img src='https://img.itch.zone/aW1nLzEzNzQ2ODkyLnBuZw==/original/Fq0ORM.png'>](https://www.npmjs.com/package/com.iron-mountain.remote-configuration)
---
## Key Scripts & Components: 
1. public struct **AppAttributes**
1. public class **EnvironmentIDText** : MonoBehaviour
1. public interface **IRemoteSetting**
   * Properties: 
      * public String ***Key***  { get; }
      * public String ***DefaultValue***  { get; }
      * public String ***Value***  { get; }
1. public class **RemoteConfigurationManager** : MonoBehaviour
1. public class **RemoteSetting**
   * Actions: 
      * public event Action ***OnValueChanged*** 
   * Properties: 
      * public String ***Key***  { get; }
      * public String ***DefaultValue***  { get; }
      * public String ***Value***  { get; }
1. public class **RemoteSettingMonoBehaviour** : MonoBehaviour
   * Properties: 
      * public String ***Key***  { get; }
      * public String ***DefaultValue***  { get; }
      * public String ***Value***  { get; }
1. public class **ScriptedRemoteConfigurationEnvironment** : ScriptableObject
   * Properties: 
      * public String ***EnvironmentID***  { get; }
   * Methods: 
      * public void ***ActivateEnvironment***()
1. public struct **UserAttributes**
