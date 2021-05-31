using System.Collections.Generic;
using Newtonsoft.Json;

namespace SavegameSystem.Storage.Serialization
{
    public interface IJsonConvertersProvider
    {
        List<JsonConverter> GetConverters();
    }
}
