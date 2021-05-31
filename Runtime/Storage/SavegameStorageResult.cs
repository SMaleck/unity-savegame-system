using SavegameSystem.Serializable;
using System;

namespace SavegameSystem.Storage
{
    public enum SavegameError
    {
        None = -1,
        Unknown = 0,
        JsonNullOrEmpty = 1,
        SavegameNull = 2,
    }

    public class SavegameLoadResult<T> where T : class
    {
        public bool Success { get; set; }
        public SavegameError Error { get; set; }
        public Exception Exception { get; set; }
        public ISavegame<T> Savegame { get; set; }

        public static SavegameLoadResult<T> FromSuccess(ISavegame<T> savegame)
        {
            return new SavegameLoadResult<T>()
            {
                Success = true,
                Error = SavegameError.None,
                Exception = null,
                Savegame = savegame
            };
        }

        public static SavegameLoadResult<T> FromError(SavegameError error, Exception e = null)
        {
            return new SavegameLoadResult<T>()
            {
                Success = false,
                Error = error,
                Exception = e,
                Savegame = null
            };
        }
    }

    public class SavegameSaveResult
    {
        public bool Success { get; set; }
        public SavegameError Error { get; set; }
        public Exception Exception { get; set; }

        public static SavegameSaveResult FromSuccess()
        {
            return new SavegameSaveResult()
            {
                Success = true,
                Error = SavegameError.None,
                Exception = null
            };
        }

        public static SavegameSaveResult FromError(SavegameError error, Exception e = null)
        {
            return new SavegameSaveResult()
            {
                Success = false,
                Error = error,
                Exception = e
            };
        }
    }
}
