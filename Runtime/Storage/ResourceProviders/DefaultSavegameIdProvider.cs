using System;

namespace SavegameSystem.Storage.ResourceProviders
{
    public class DefaultSavegameIdProvider : ISavegameIdProvider
    {
        public string CreateId() => new Guid().ToString();
    }
}
