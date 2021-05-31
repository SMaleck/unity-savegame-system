namespace SavegameSystem.Serializable.Creation
{
    public class SavegameFactory : ISavegameFactory
    {
        private readonly ISavegameMetaDataFactory _metaDataFactory;
        private readonly ISavegameContentFactory _contentFactory;

        public SavegameFactory(
            ISavegameMetaDataFactory metaDataFactory,
            ISavegameContentFactory contentFactory)
        {
            _metaDataFactory = metaDataFactory;
            _contentFactory = contentFactory;
        }

        public ISavegame<T> Create<T>() where T : class
        {
            return new Savegame<T>()
            {
                MetaData = _metaDataFactory.Create(),
                Content = _contentFactory.Create<T>()
            };
        }
    }
}
