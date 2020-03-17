using Newtonsoft.Json;
using PABXMonitor.PABX.Discover.Model;
using System;
using System.Linq;
using TCX.Configuration;

namespace PABXMonitor.PABX.Discover
{
    public class Trunks
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(ModelData(), Formatting.Indented);
        }

        private ListModel<TrunkModel> ModelData()
        {
            int loDummy = 0;
            var loData = PhoneSystem.Root.GetExternalLines()
                      .Where(x => Int32.TryParse(x.Number, out loDummy))
                       .Select(x => new TrunkModel()
                       {
                           Extension = loDummy,
                           Direction = x.Direction.ToString(),
                           ExternalNumber = x.ExternalNumber,
                       }).OrderBy(x => x.Extension).ToList();

            return new ListModel<TrunkModel>()
            {
                Data = loData,
            };
        }
    }
}