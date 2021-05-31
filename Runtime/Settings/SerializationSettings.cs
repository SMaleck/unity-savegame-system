using Newtonsoft.Json;
using SavegameSystem.Storage.Serialization;

namespace SavegameSystem.Settings
{
    public class SerializationSettings : ISerializationSettings
    {
        public JsonSerializerSettings DefaultSettings { get; }

        public SerializationSettings(IJsonConvertersProvider convertersProvider)
        {
            DefaultSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Include,
                MissingMemberHandling = MissingMemberHandling.Error,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                Converters = convertersProvider.GetConverters()
            };
        }
    }
}
