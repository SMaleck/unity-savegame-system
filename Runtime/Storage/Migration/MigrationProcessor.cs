using Newtonsoft.Json.Linq;
using SavegameSystem.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SavegameSystem.Storage.Migration
{
    public class MigrationProcessor : IMigrationProcessor
    {
        private readonly ISavegameLogger _logger;
        private readonly IMigrationDebugger _migrationDebugger;
        private readonly IReadOnlyList<ISavegameMigration> _migrations;

        public MigrationProcessor(
            ISavegameLogger logger,
            IMigrationDebugger migrationDebugger,
            List<ISavegameMigration> migrations)
        {
            _logger = logger;
            _migrationDebugger = migrationDebugger;
            _migrations = migrations.OrderBy(e => e.Version).ToArray();

            ValidateMigrations();
        }

        public string Process(string savegameJson)
        {
            try
            {
                var savegame = JObject.Parse(savegameJson);
                var version = GetVersion(savegame);

                foreach (var migration in _migrations)
                {
                    if (migration.Version <= version)
                    {
                        continue;
                    }

                    _migrationDebugger.BeforeMigration(version, migration.Version, savegame);
                    Migrate(savegame, migration);
                    _migrationDebugger.AfterMigration(version, migration.Version, savegame);
                }

                return savegame.ToString();
            }
            catch (Exception e)
            {
                _logger.Error($"Migration Failed: {e.Message}");
                throw e;
            }
        }

        private void ValidateMigrations()
        {
            if (!_migrations.Any())
            {
                return;
            }

            if (_migrations[0].Version != 2)
            {
                _logger.Warn($"Detected incoherent Version for 1st Migration: [{ _migrations[0].Version}] Expected: [2]");
            }

            if (_migrations.Count >= 2)
            {
                for (var i = 1; i < _migrations.Count; i++)
                {
                    if (_migrations[i - 1].Version != _migrations[i].Version - 1)
                    {
                        _logger.Warn($"Detected gap in Migration Versions. Missing: { _migrations[i].Version - 1}");
                    }
                }
            }
        }

        private int GetVersion(JObject savegame)
        {
            return savegame["MetaData"].Value<int>("Version");
        }

        private void Migrate(JObject savegame, ISavegameMigration migration)
        {
            savegame = migration.Migrate(savegame);
            savegame["MetaData"]["Version"] = migration.Version;
        }
    }
}
