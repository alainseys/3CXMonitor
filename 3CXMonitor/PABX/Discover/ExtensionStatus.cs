using System.Linq;
using TCX.Configuration;

namespace PABXMonitor.PABX.Discover
{
    internal class ExtensionStatus
    {
        public string Registered(string paExtension)
        {
            var loDNs = PhoneSystem.Root.GetDN();

            foreach (var loDN in loDNs)
            {
                if (loDN.Number == paExtension)
                {
                    if (loDN.IsRegistered)
                    {
                        return "Registered";
                    }
                    else
                    {
                        return "Not Registered";
                    }
                }
            }

            return "Removed";
        }

        public string RegisteredX(string paExtension)
        {
            var loData = PhoneSystem.Root.GetExtensions()
                       .Where(x => x.Number == paExtension).SingleOrDefault();

            if (loData == null)
                return "Removed";
            else if (loData.IsRegistered)
            {
                return "Registered";
            }
            else
            {
                return "Not Registered";
            }
        }
    }
}