using Newtonsoft.Json.Linq;

namespace SavegameSystem.Storage.Migration
{
    public interface IMigrationDebugger
    {
        void BeforeMigration(int fromVersion, int toVersion, JObject savegame);
        void AfterMigration(int fromVersion, int toVersion, JObject savegame);
    }
}