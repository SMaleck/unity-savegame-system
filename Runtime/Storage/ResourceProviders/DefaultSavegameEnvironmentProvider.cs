namespace SavegameSystem.Storage.ResourceProviders
{
    public class DefaultSavegameEnvironmentProvider : ISavegameEnvironmentProvider
    {
        public bool IsDebug => UnityEngine.Debug.isDebugBuild;
    }
}
