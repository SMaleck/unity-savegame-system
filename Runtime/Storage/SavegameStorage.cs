using SavegameSystem.Logging;
using SavegameSystem.Serializable;
using SavegameSystem.Storage.Middlewares;
using SavegameSystem.Storage.Middlewares.PostRead;
using SavegameSystem.Storage.Middlewares.PreWrite;
using SavegameSystem.Storage.Middlewares.Read;
using SavegameSystem.Storage.Middlewares.Write;
using SavegameSystem.Storage.Migration;
using SavegameSystem.Storage.ResourceProviders;
using SavegameSystem.Storage.Serialization;
using SavegameSystem.Storage.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SavegameSystem.Storage
{
    /// <summary>
    /// Represents a single storage target, e.g. local disk, cloud, etc.
    /// </summary>
    public class SavegameStorage : ISavegameStorage
    {
        private readonly ISavegameLogger _logger;
        private readonly ISavegameAccessLockProvider _accessLockProvider;
        private readonly ISavegameStorageStrategy _storageStrategy;
        private readonly IMigrationProcessor _migrationProcessor;
        private readonly ISerializationProcessor _serializationProcessor;

        private readonly ISavegameReadMiddleware[] _readMiddlewares;
        private readonly ISavegamePostReadMiddleware[] _postReadMiddlewares;
        private readonly ISavegamePreWriteMiddleware[] _preWriteMiddlewares;
        private readonly ISavegameWriteMiddleware[] _writeMiddlewares;

        public SavegameStorage(
            ISavegameLogger logger,
            ISavegameAccessLockProvider accessLockProvider,
            ISavegameStorageStrategy storageStrategy,
            IMigrationProcessor migrationProcessor,
            ISerializationProcessor serializationProcessor,
            List<ISavegameStorageMiddleware> middlewares)
        {
            _logger = logger;
            _accessLockProvider = accessLockProvider;
            _storageStrategy = storageStrategy;
            _migrationProcessor = migrationProcessor;
            _serializationProcessor = serializationProcessor;

            _readMiddlewares = FilterMiddleware<ISavegameReadMiddleware>(middlewares);
            _postReadMiddlewares = FilterMiddleware<ISavegamePostReadMiddleware>(middlewares);
            _preWriteMiddlewares = FilterMiddleware<ISavegamePreWriteMiddleware>(middlewares);
            _writeMiddlewares = FilterMiddleware<ISavegameWriteMiddleware>(middlewares);
        }

        private T[] FilterMiddleware<T>(IReadOnlyList<ISavegameStorageMiddleware> middlewares)
            where T : ISavegameStorageMiddleware
        {
            return middlewares
                .OfType<T>()
                .OrderBy(e => e.ExecutionOrder)
                .ToArray();
        }

        public SavegameLoadResult<T> Load<T>() where T : class
        {
            lock (_accessLockProvider)
            {
                try
                {
                    var savegame = (ISavegame<T>)null;

                    var savegameJson = _storageStrategy.Read();
                    if (string.IsNullOrWhiteSpace(savegameJson))
                    {
                        return SavegameLoadResult<T>.FromError(SavegameError.JsonNullOrEmpty);
                    }

                    savegameJson = ExecuteReadMiddlewares(savegameJson);

                    var migratedSavegame = _migrationProcessor.Process(savegameJson);
                    savegame = _serializationProcessor.Deserialize<T>(migratedSavegame);

                    savegame = ExecutePostReadMiddlewares(savegame);

                    return SavegameLoadResult<T>.FromSuccess(savegame);
                }
                catch (Exception e)
                {
                    _logger.Error("Failed to Load Savegame");
                    _logger.Error(e);

                    return SavegameLoadResult<T>.FromError(SavegameError.Unknown, e);
                }
            }
        }

        public SavegameSaveResult Save<T>(ISavegame<T> savegame) where T : class
        {
            lock (_accessLockProvider)
            {
                try
                {
                    savegame = ExecutePreWriteMiddlewares(savegame);
                    var savegameJson = _serializationProcessor.Serialize<T>(savegame);
                    savegameJson = ExecuteWriteMiddlewares(savegameJson);

                    _storageStrategy.Write(savegameJson);
                }
                catch (Exception e)
                {
                    _logger.Error("Failed to Save Savegame");
                    _logger.Error(e);

                    return SavegameSaveResult.FromError(SavegameError.Unknown, e);
                }

                return SavegameSaveResult.FromSuccess();
            }
        }

        private string ExecuteReadMiddlewares(string savegame)
        {
            for (var i = 0; i < _readMiddlewares.Length; i++)
            {
                var mw = _readMiddlewares[i];
                savegame = mw.Process(savegame);
            }

            return savegame;
        }

        private ISavegame<T> ExecutePostReadMiddlewares<T>(ISavegame<T> savegame)
            where T : class
        {
            for (var i = 0; i < _postReadMiddlewares.Length; i++)
            {
                var mw = _postReadMiddlewares[i];
                savegame = mw.Process(savegame);
            }

            return savegame;
        }

        private ISavegame<T> ExecutePreWriteMiddlewares<T>(ISavegame<T> savegame)
            where T : class
        {
            for (var i = 0; i < _preWriteMiddlewares.Length; i++)
            {
                var mw = _preWriteMiddlewares[i];
                savegame = mw.Process(savegame);
            }

            return savegame;
        }

        private string ExecuteWriteMiddlewares(string savegame)
        {
            for (var i = 0; i < _writeMiddlewares.Length; i++)
            {
                var mw = _writeMiddlewares[i];
                savegame = mw.Process(savegame);
            }

            return savegame;
        }
    }
}
