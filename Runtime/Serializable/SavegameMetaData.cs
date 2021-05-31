using System;

namespace SavegameSystem.Serializable
{
    [Serializable]
    public class SavegameMetaData
    {
        public string Id { get; set; }
        public int Version { get; set; }
        
        public DateTime CreatedAtUtc { get; set; }
        public DateTime UpdatedAtUtc { get; set; }

        public string CreatedAtClientVersion { get; set; }
        public string CreatedAtClientBuild { get; set; }
        
        public string ClientVersion { get; set; }
        public string ClientBuild { get; set; }
    }
}
