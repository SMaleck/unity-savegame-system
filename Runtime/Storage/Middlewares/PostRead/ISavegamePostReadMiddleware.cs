using SavegameSystem.Serializable;

namespace SavegameSystem.Storage.Middlewares.PostRead
{
    public interface ISavegamePostReadMiddleware : ISavegameStorageMiddleware
    {
        ISavegame<T> Process<T>(ISavegame<T> savegame)
            where T : class;
    }
}
