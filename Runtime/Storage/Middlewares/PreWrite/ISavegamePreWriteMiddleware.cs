using SavegameSystem.Serializable;

namespace SavegameSystem.Storage.Middlewares.PreWrite
{
    public interface ISavegamePreWriteMiddleware : ISavegameStorageMiddleware
    {
        ISavegame<T> Process<T>(ISavegame<T> savegame) where T : class;
    }
}
