using SavegameSystem.Storage.ResourceProviders;

namespace SavegameSystem.Logging
{
    public class DefaultSavegameLogger : ISavegameLogger
    {
        private const string Prefix = "[Savegame]";
        private readonly ISavegameEnvironmentProvider _environmentProvider;

        private bool IsDebug => _environmentProvider.IsDebug;

        public DefaultSavegameLogger(ISavegameEnvironmentProvider environmentProvider)
        {
            _environmentProvider = environmentProvider;
        }

        public void Log(object payload)
        {
            if (IsDebug)
            {
                UnityEngine.Debug.Log($"{Prefix} {ToMessage(payload)}");
            }
        }

        public void Warn(object payload)
        {
            if (IsDebug)
            {
                UnityEngine.Debug.LogWarning($"{Prefix} {ToMessage(payload)}");
            }
        }

        public void Error(object payload)
        {
            if (IsDebug)
            {
                UnityEngine.Debug.LogError($"{Prefix} {ToMessage(payload)}");
            }
        }

        private string ToMessage(object payload)
        {
            return payload?.ToString() ?? string.Empty;
        }
    }
}
