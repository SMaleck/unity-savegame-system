using SavegameSystem.Settings;
using System.IO;

namespace SavegameSystem.Storage.ResourceProviders
{
    public class DefaultSavegamePathProvider : ISavegamePathProvider
    {
        private readonly string _filePath;

        public DefaultSavegamePathProvider(ISavegameSettings savegameSettings)
        {
            _filePath = Path.Combine(savegameSettings.Path, savegameSettings.Filename);
        }

        public string GetFilePath() => _filePath;
    }
}
