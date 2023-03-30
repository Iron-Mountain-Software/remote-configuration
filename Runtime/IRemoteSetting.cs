using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellBoundAR.RemoteConfiguration
{
    public interface IRemoteSetting
    {
        public string Key { get; }
        public string DefaultValue { get; }
        public string Value { get; }
    }
}
