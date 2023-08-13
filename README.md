Remote Configuration
====================

_A library for interfacing with Unity's built in Remote Config service._

SOME CODING REQUIRED

Unity's Remote Config service is an awesome solution that I've used across multiple projects, but it requires some boilerplate code to get up and running. This package contains those scripts that I use to make it work.

Use Cases
---------

*   Quickly standing up Unity's Remote Config service. 
*   An improved system for switching environments at runtime.
*   An improved system for handling and using remote config variables in scripts.

Directions for Use
------------------

1.  Place the RemoteConfigurationManager on a gameobject in your scene. This component is responsible for initializing the service and setting the environment. 
2.  If you want the RemoteConfigurationManager to initialize the environment (you probably do): 
    1.  Check the box for "Initialize Environment on Awake"
    2.  Create a ScriptedRemoteConfigurationEnvironment instance (Create > Scriptable Objects > Remote Config Environment).
    3.  Copy your environment ID for the remote config dashboard into this object. 
    4.  Drag the ScriptedRemoteConfigurationEnvironment onto the RemoteConfigurationManager into the "Initialization Environment" property.
3.  There are 2 new ways to access your remote configuration variables:
    1.  Create an instance of RemoteSetting in a script:  
        1.    RemoteSetting mySetting = new RemoteSetting("key", "default value");
        2.  string value = mySetting.Value;
        3.  mySetting.OnValueChanged += ValueChangedHandler;
    2.  Add a RemoteSettingMonobehaviour onto a gameobject, and configure the key and defaultValue fields. In another script:
        1.  RemoteSettingMonobehaviour mySetting;
        2.  string value = mySetting.Value;  
            
        3.  mySetting.OnValueChanged += ValueChangedHandler;

Key Components
--------------

1.  RemoteConfigurationManager
    1.  Used to initialize the system and fetch the remote settings.
2.  ScriptedRemoteConfigurationEnvironment
    1.  Represents an environment, and used to switch between them.
3.  IRemoteSetting, RemoteSetting, and RemoteSettingMonobehaviour
    1.  Used to work with remote configuration variables.
4.  EnvironmentIDText
    1.  Drop onto a gameobject with a text component to make the text reflect the active environment ID.