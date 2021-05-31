namespace SavegameSystem.Settings
{
    public interface ISavegameSettings
    {
        string Path { get; }
        string Filename { get; }
        bool UseCompression { get; }
    }
}
