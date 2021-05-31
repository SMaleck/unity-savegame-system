namespace SavegameSystem.Storage.ResourceProviders
{
    public interface ISavegameClientVersionProvider
    {
        string ClientVersion { get; }
        string ClientBuild { get; }
    }
}
