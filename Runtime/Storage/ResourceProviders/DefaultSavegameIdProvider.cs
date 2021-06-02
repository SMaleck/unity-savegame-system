using System;

namespace SavegameSystem.Storage.ResourceProviders
{
    public class DefaultSavegameIdProvider : ISavegameIdProvider
    {
        public string CreateId() => Guid.NewGuid().ToString();
    }
}
