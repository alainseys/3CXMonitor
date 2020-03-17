using Newtonsoft.Json;
using System.Collections.Generic;
using TCX.Configuration;

namespace PABXMonitor.PABX
{
    public abstract class Base<T>
        where T : class
    {
        protected Base()
        {
            PhoneSystem.Root.GetDN();
        }

      
        protected abstract T Data { get; }

        
    }
}