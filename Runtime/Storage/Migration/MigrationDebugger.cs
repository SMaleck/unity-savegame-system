using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SavegameSystem.Logging;
using SavegameSystem.Storage.ResourceProviders;
using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace SavegameSystem.Storage.Migration
{
    public class MigrationDebugger : IMigrationDebugger
    {
        private readonly ISavegameEnvironmentProvider _environmentProvider;
        private readonly ISavegameLogger _logger;

        private bool IsDebug => _environmentProvider.IsDebug;
        private readonly string _migrationDiffPath;
        private readonly Stopwatch _stopwatch;

        public MigrationDebugger(
            ISavegameEnvironmentProvider environmentProvider,
            ISavegameLogger logger)
        {
            _environmentProvider = environmentProvider;
            _logger = logger;

            _stopwatch = new Stopwatch();
            _migrationDiffPath = Path.Combine(Application.persistentDataPath, "MigrationDiff");
        }

        public void BeforeMigration(int fromVersion, int toVersion, JObject savegame)
        {
            if (!IsDebug)
            {
                return;
            }

            _stopwatch.Start();
            _logger.Log($"Starting migration from [{fromVersion}] to [{toVersion}]");

            var migrationFilePath = Path.Combine(_migrationDiffPath, $"V{toVersion}_Before");
            Write(migrationFilePath, savegame);
            _logger.Log($"Saved current JSON in {migrationFilePath}");
        }

        public void AfterMigration(int fromVersion, int toVersion, JObject savegame)
        {
            if (!IsDebug)
            {
                return;
            }

            _stopwatch.Stop();
            _logger.Log($"Completed migration from [{fromVersion}] to [{toVersion}] in {_stopwatch.ElapsedMilliseconds}ms");

            var migrationFilePath = Path.Combine(_migrationDiffPath, $"V{toVersion}_After");
            Write(migrationFilePath, savegame);
            _logger.Log($"Saved current JSON in {migrationFilePath}");
        }

        private void Write(string filePath, JObject savegame)
        {
            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(filePath, savegame.ToString(Formatting.Indented));
        }
    }
}
