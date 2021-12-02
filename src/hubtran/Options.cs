using CommandLine;

namespace hubtran
{
    public class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }
        [Option('f', "fileFilter", Required = false, HelpText = "Filter file condition", Default = "*.json")]
        public string FileFilter { get; set; }
        [Option('i', "inputFolder", Required = true, HelpText = "Input folder to read files from")]
        public string InputFolder { get; set; }
        [Option('o', "outputFolder", Required = true, HelpText = "Output folder to write files to")]
        public string OutputFolder { get; set; }        
    }
}