using System.Collections.Generic;
using Newtonsoft.Json;

namespace SavegameSystem.Storage.Serialization
{
    public class JsonConvertersProvider : IJsonConvertersProvider
    {
        private readonly List<JsonConverter> _converters;

        public JsonConvertersProvider()
        {
            _converters = new List<JsonConverter>();
        }

        public List<JsonConverter> GetConverters()
        {
            return _converters;
        }
    }
}
