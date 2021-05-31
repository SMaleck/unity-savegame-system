using Newtonsoft.Json.Linq;
using SavegameSystem.Settings;
using SavegameSystem.Utility;

namespace SavegameSystem.Storage.Middlewares.Write
{
    public class CompressorMiddleware : AbstractStorageMiddleware, ISavegameWriteMiddleware
    {
        private readonly ISavegameSettings _settings;

        public CompressorMiddleware(ISavegameSettings settings)
            : base(SavegameConstants.CompressorExecutionOrder)
        {
            _settings = settings;
        }

        public string Process(string savegameJson)
        {
            if (!_settings.UseCompression)
            {
                return savegameJson;
            }

            var savegame = JObject.Parse(savegameJson);
            savegame["Content"] = GetCompressedContent(savegame);

            return savegame.ToString();
        }

        private JValue GetCompressedContent(JObject savegame)
        {
            var content = (JObject)savegame["Content"];
            var compressedContent = GzipCompressor.Compress(content.ToString());

            return new JValue(compressedContent);
        }
    }
}
