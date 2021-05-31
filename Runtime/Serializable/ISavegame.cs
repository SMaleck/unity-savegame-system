namespace SavegameSystem.Serializable
{
    public interface ISavegame<T> where T : class
    {
        SavegameMetaData MetaData { get; }
        public T Content { get; set; }
    }
}
