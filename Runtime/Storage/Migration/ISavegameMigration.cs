using Newtonsoft.Json.Linq;

namespace SavegameSystem.Storage.Migration
{
    public interface ISavegameMigration
    {
        int Version { get; }
        JObject Migrate(JObject savegameJson);
    }
}