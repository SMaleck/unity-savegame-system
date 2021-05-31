using SavegameSystem.Storage.ResourceProviders;

namespace SavegameSystem.Serializable.Creation
{
    public class SavegameMetaDataFactory : ISavegameMetaDataFactory
    {
        private readonly ISavegameIdProvider _idProvider;
        private readonly ISavegameVersionProvider _versionProvider;
        private readonly ISavegameClientVersionProvider _clientVersionProvider;
        private readonly ISavegameTimeProvider _savegameTimeProvider;

        public SavegameMetaDataFactory(
            ISavegameIdProvider idProvider,
            ISavegameVersionProvider versionProvider,
            ISavegameClientVersionProvider clientVersionProvider,
            ISavegameTimeProvider savegameTimeProvider)
        {
            _idProvider = idProvider;
            _versionProvider = versionProvider;
            _clientVersionProvider = clientVersionProvider;
            _savegameTimeProvider = savegameTimeProvider;
        }

        public SavegameMetaData Create()
        {
            return new SavegameMetaData()
            {
                Id = _idProvider.CreateId(),
                Version = _versionProvider.Version,
                CreatedAtUtc = _savegameTimeProvider.UtcNow,
                UpdatedAtUtc = _savegameTimeProvider.UtcNow,
                CreatedAtClientVersion = _clientVersionProvider.ClientVersion,
                CreatedAtClientBuild = _clientVersionProvider.ClientBuild,
                ClientVersion = _clientVersionProvider.ClientVersion,
                ClientBuild = _clientVersionProvider.ClientBuild
            };
        }
    }
}
