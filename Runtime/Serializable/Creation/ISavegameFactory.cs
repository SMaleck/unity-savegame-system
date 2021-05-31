namespace SavegameSystem.Serializable.Creation
{
    public interface ISavegameFactory
    {
        ISavegame<T> Create<T>() where T : class;
    }
}
