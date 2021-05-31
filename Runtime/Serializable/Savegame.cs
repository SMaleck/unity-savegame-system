using System;

namespace SavegameSystem.Serializable
{
    [Serializable]
    public class Savegame<T> : ISavegame<T> where T : class
    {
        public SavegameMetaData MetaData { get; set; }
        public T Content { get; set; }
    }
}
