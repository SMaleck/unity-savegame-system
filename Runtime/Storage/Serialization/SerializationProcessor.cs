using Newtonsoft.Json;
using SavegameSystem.Serializable;
using SavegameSystem.Settings;

namespace SavegameSystem.Storage.Serialization
{
    public class SerializationProcessor : ISerializationProcessor
    {
        private readonly ISerializationSettings _serializationSettings;

        public SerializationProcessor(ISerializationSettings serializationSettings)
        {
            _serializationSettings = serializationSettings;
        }

        public string Serialize<T>(ISavegame<T> savegame)
            where T : class
        {
            return JsonConvert.SerializeObject(
                savegame,
                _serializationSettings.DefaultSettings);
        }

        public Savegame<T> Deserialize<T>(string savegameJson)
            where T : class
        {
            return JsonConvert.DeserializeObject<Savegame<T>>(
                savegameJson,
                _serializationSettings.DefaultSettings);
        }
    }
}
