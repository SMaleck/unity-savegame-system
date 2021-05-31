using Newtonsoft.Json.Linq;
using SavegameSystem.Logging;
using SavegameSystem.Settings;
using SavegameSystem.Utility;
using System;

namespace SavegameSystem.Storage.Middlewares.Read
{
    public class DecompressorMiddleware : AbstractStorageMiddleware, ISavegameReadMiddleware
    {
        private readonly ISavegameLogger _logger;

        public DecompressorMiddleware(ISavegameLogger logger)
            : base(SavegameConstants.DecompressorExecutionOrder)
        {
            _logger = logger;
        }

        public string Process(string savegameJson)
        {
            // Always attempt decompression, regardless of settings
            // Otherwise a previously compressed savegame cannot be loaded anymore

            var savegame = JObject.Parse(savegameJson);
            savegame["Content"] = ProcessContent(savegame);

            return savegame.ToString();
        }

        private JObject ProcessContent(JObject savegame)
        {
            try
            {
                var contentJson = savegame["Content"]?.ToString();
                var decompressedContent = GzipCompressor.Decompress(contentJson);
                return JObject.Parse(decompressedContent);
            }
            catch (FormatException)
            {
                _logger.Warn("Savegame Content was not a valid Base64 string, probably the savegame was not compressed. Returning as is.");
                return (JObject)savegame["Content"];
            }
        }
    }
}
