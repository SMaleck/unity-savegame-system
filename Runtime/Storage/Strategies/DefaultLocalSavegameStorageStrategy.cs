using SavegameSystem.Logging;
using SavegameSystem.Storage.ResourceProviders;
using System.IO;

namespace SavegameSystem.Storage.Strategies
{
    public class DefaultLocalSavegameStorageStrategy : ISavegameStorageStrategy
    {
        private readonly ISavegameLogger _logger;
        private readonly ISavegamePathProvider _pathProvider;
        private string FilePath => _pathProvider.GetFilePath();

        public DefaultLocalSavegameStorageStrategy(
            ISavegameLogger logger,
            ISavegamePathProvider pathProvider)
        {
            _logger = logger;
            _pathProvider = pathProvider;
        }

        public string Read()
        {
            _logger.Log($"Reading savegame from {FilePath}");

            if (!File.Exists(FilePath))
            {
                return string.Empty;
            }

            return File.ReadAllText(FilePath);
        }

        public void Write(string serializedSavegame)
        {
            _logger.Log($"Writing savegame to {FilePath}");

            var directory = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(FilePath, serializedSavegame);
        }
    }
}
