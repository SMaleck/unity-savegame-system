namespace SavegameSystem.Storage.Migration
{
    public interface IMigrationProcessor
    {
        string Process(string savegameJson);
    }
}