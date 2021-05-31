namespace SavegameSystem.Logging
{
    public interface ISavegameLogger
    {
        void Log(object payload);
        void Warn(object payload);
        void Error(object payload);
    }
}
