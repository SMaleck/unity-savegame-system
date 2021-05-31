using SavegameSystem.Serializable;

namespace SavegameSystem.Storage
{
    public interface ISavegameStorage
    {
        SavegameLoadResult<T> Load<T>() where T : class;
        SavegameSaveResult Save<T>(ISavegame<T> savegame) where T : class;
    }
}