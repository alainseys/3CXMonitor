using Newtonsoft.Json;
using PABXMonitor.PABX.Discover.Model;
using System;
using System.Linq;
using TCX.Configuration;

namespace PABXMonitor.PABX.Discover
{
    public class Extensions //: Base<List<ExtensionModel>>
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(ModelData(), Formatting.Indented);
        }

        private ListModel<ExtensionModel> ModelData()
        {
            int loDummy = 0;
            var loData = PhoneSystem.Root.GetExtensions()
                       .Where(x => Int32.TryParse(x.Number, out loDummy))
                       .Select(x => new ExtensionModel()
                       {
                           Name = x.ToString(),
                           Extension = loDummy,
                       }).OrderBy(x => x.Extension).ToList();

            return new ListModel<ExtensionModel>()
            {
                Data = loData,
            };
        }
    }
}