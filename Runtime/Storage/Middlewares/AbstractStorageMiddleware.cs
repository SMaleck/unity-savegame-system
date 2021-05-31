using SavegameSystem.Settings;

namespace SavegameSystem.Storage.Middlewares
{
    public abstract class AbstractStorageMiddleware : ISavegameStorageMiddleware
    {
        public int ExecutionOrder { get; }

        protected AbstractStorageMiddleware(int executionOrder = SavegameConstants.DefaultExecutionOrder)
        {
            ExecutionOrder = executionOrder;
        }
    }
}
