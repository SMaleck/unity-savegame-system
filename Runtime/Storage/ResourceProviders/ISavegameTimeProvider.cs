using System;

namespace SavegameSystem.Storage.ResourceProviders
{
    public interface ISavegameTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
