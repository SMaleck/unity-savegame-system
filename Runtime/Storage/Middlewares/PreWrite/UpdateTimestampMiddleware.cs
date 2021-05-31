using SavegameSystem.Serializable;
using SavegameSystem.Storage.ResourceProviders;

namespace SavegameSystem.Storage.Middlewares.PreWrite
{
    public class UpdateTimestampMiddleware : AbstractStorageMiddleware, ISavegamePreWriteMiddleware
    {
        private readonly ISavegameTimeProvider _timeProvider;

        public UpdateTimestampMiddleware(ISavegameTimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public ISavegame<T> Process<T>(ISavegame<T> savegame)
            where T : class
        {
            savegame.MetaData.UpdatedAtUtc = _timeProvider.UtcNow;

            return savegame;
        }
    }
}
