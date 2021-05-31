using Newtonsoft.Json;

namespace SavegameSystem.Settings
{
    public interface ISerializationSettings
    {
        JsonSerializerSettings DefaultSettings { get; }
    }
}