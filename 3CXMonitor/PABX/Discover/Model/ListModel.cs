using Newtonsoft.Json;
using System.Collections.Generic;

namespace PABXMonitor.PABX.Discover.Model

{
    public class ListModel<T>
        where T:class
    {
        [JsonProperty("data")]
        public List<T> Data { get; set; }
    }
}