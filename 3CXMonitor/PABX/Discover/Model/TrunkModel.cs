using Newtonsoft.Json;

namespace PABXMonitor.PABX.Discover.Model
{
    internal class TrunkModel
    {
        [JsonProperty("{#TRK.DIRECTION}")]
        public string Direction { get; set; }

        [JsonProperty("{#TRK.NUMBER}")]
        public int Extension { get; set; }

        [JsonProperty("{#TRK.EXTERNALNUMBER}")]
        public string ExternalNumber { get; set; }
    }
}