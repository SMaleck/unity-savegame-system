using SavegameSystem.Serializable;
using SavegameSystem.Storage.ResourceProviders;

namespace SavegameSystem.Storage.Middlewares.PreWrite
{
    public class UpdateVersionMiddleware : AbstractStorageMiddleware, ISavegamePreWriteMiddleware
    {
        private readonly ISavegameClientVersionProvider _clientVersionProvider;

        public UpdateVersionMiddleware(ISavegameClientVersionProvider clientVersionProvider)
        {
            _clientVersionProvider = clientVersionProvider;
        }

        public ISavegame<T> Process<T>(ISavegame<T> savegame)
            where T : class
        {
            savegame.MetaData.ClientVersion = _clientVersionProvider.ClientVersion;
            savegame.MetaData.ClientBuild = _clientVersionProvider.ClientBuild;

            return savegame;
        }
    }
}
