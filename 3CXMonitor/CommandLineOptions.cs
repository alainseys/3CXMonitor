using CommandLine;

namespace PABXMonitor
{
    public class CommandLineOptions
    {
        [Option('e', "extensions", Required = false, HelpText = "Get a list of extensions")]
        public bool Extensions { get; set; }

        [Option('s', "status", Required = false, HelpText = "Status of an extension")]
        public bool Status { get; set; }

        [Option('t', "trunks", Required = false, HelpText = "Get a list of trunks")]
        public bool Trunks { get; set; }
    }
}