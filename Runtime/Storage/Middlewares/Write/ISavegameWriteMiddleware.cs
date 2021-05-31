namespace SavegameSystem.Storage.Middlewares.Write
{
    public interface ISavegameWriteMiddleware : ISavegameStorageMiddleware
    {
        string Process(string savegameJson);
    }
}
