using CommandLine;
using System;
using System.IO;
using TCX.Configuration;

namespace PABXMonitor
{
    internal class Program
    {
        private static void LoadAndApplySettings(string filePath)
        {
            var loSuffix = new Random(Environment.TickCount);
            PhoneSystem.ApplicationName = "CXMonitorStd";//any name
            PhoneSystem.ApplicationName = PhoneSystem.ApplicationName + loSuffix.Next().ToString();

            #region phone system initialization(init db server)

            var value = Utilities.GetKeyValue("ConfService", "ConfPort", filePath);
            var port = 0;
            PhoneSystem.CfgServerHost = "127.0.0.1";
            if (!string.IsNullOrEmpty(value))
            {
                int.TryParse(value.Trim(), out port);
                PhoneSystem.CfgServerPort = port;
            }
            value = Utilities.GetKeyValue("ConfService", "confUser", filePath);
            if (!string.IsNullOrEmpty(value))
                PhoneSystem.CfgServerUser = value;
            value = Utilities.GetKeyValue("ConfService", "confPass", filePath);
            if (!string.IsNullOrEmpty(value))
                PhoneSystem.CfgServerPassword = value;

            #endregion phone system initialization(init db server)
        }

        private static int Main(string[] args)
        {
            try
            {
                var loSettingsFile = @".\3CXPhoneSystem.ini";
                if (!File.Exists(loSettingsFile))
                {
                    //this code expects 3CXPhoneSystem.ini in current directory.
                    //it can be taken from the installation folder (find it in Program Files/3CXPhone System/instance1/bin for in premiss installation)
                    //or this application can be run with current directory set to location of 3CXPhoneSystem.ini

                    //v14 (cloud and in premiss) installation has changed folder structure.
                    //3CXPhoneSystem.ini which contains connectio information is located in
                    //<Program Files>/3CX Phone System/instanceN/Bin folder.
                    //in premiss instance files are located in <Program Files>/3CX Phone System/instance1/Bin
                    throw new Exception("Cannot find 3CXPhoneSystem.ini");
                }

                LoadAndApplySettings(loSettingsFile);

                Parser.Default.ParseArguments<CommandLineOptions>(args)
                   .WithParsed<CommandLineOptions>(o =>
                   {
                       if (o.Extensions)
                       {
                           Console.WriteLine(new PABX.Discover.Extensions().ToString());
                       }
                       else if (o.Trunks)
                       {
                           Console.WriteLine(new PABX.Discover.Trunks().ToString());
                       }
                       else if (o.Status)
                       {
                           Console.WriteLine(new PABX.Discover.ExtensionStatus().Registered(args[1]));
                       }
                       else
                       {
                           try
                           {
                               Console.WriteLine(string.Format("Connecting to: [{0}]:[{1}] [{2}][{3}] - [{4}]", PhoneSystem.CfgServerHost, PhoneSystem.CfgServerPort, PhoneSystem.CfgServerUser, PhoneSystem.CfgServerPassword, PhoneSystem.ApplicationName));
                               var loDNS = PhoneSystem.Root.GetDN();
                               foreach (var loDN in loDNS)
                               {
                                   Console.WriteLine("\t" + loDN);
                                   Console.WriteLine(string.Format("\t\t{0}:{1}",
                                                            loDN.IsRegistered,
                                                            loDN.Number
                                                           ));
                               }

                               Samples.DisplayAllSample.Run();
                               Console.WriteLine("Connection Passed");
                           }
                           catch (Exception)
                           {
                               Console.WriteLine("Connection Failed");
                               throw;
                           }
                       }
                   });
                PhoneSystem.Root.Disconnect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 1;
            }
            return 0;
        }
    }
}