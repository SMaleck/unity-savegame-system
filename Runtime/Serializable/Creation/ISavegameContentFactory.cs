namespace SavegameSystem.Serializable.Creation
{
    public interface ISavegameContentFactory
    {
        T Create<T>() where T : class;
    }
}
