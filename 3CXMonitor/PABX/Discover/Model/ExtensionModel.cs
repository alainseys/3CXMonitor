using Newtonsoft.Json;

namespace PABXMonitor.PABX.Discover.Model
{
    public class ExtensionModel
    {
        [JsonProperty("{#EXT.NUMBER}")]
        public int Extension { get; set; }

        [JsonProperty("{#EXT.NAME}")]
        public string Name { get; set; }

    }
}