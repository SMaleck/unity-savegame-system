using SavegameSystem.Serializable;

namespace SavegameSystem.Storage.Serialization
{
    public interface ISerializationProcessor
    {
        string Serialize<T>(ISavegame<T> savegame)
            where T : class;

        Savegame<T> Deserialize<T>(string savegameJson)
            where T : class;
    }
}