namespace SavegameSystem.Storage.Middlewares
{
    public interface ISavegameStorageMiddleware
    {
        public int ExecutionOrder { get; }
    }
}
