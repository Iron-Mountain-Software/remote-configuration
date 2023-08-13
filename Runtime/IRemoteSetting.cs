namespace IronMountain.RemoteConfiguration
{
    public interface IRemoteSetting
    {
        public string Key { get; }
        public string DefaultValue { get; }
        public string Value { get; }
    }
}
