namespace SavegameSystem.Storage.Strategies
{
    public interface ISavegameStorageStrategy
    {
        string Read();
        void Write(string serializedSavegame);
    }
}
