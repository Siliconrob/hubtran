using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using CommandLine;

namespace hubtran
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DivInput, DivOutput>());
            var mapper = config.CreateMapper();
            await Parser.Default.ParseArguments<Options>(args)
                .WithParsedAsync(async o =>
                {
                    Directory.CreateDirectory(o.OutputFolder);
                    foreach (var inputFile in Directory.GetFiles(o.InputFolder, o.FileFilter))
                    {
                        var outputFile = await FileFormatter.ConvertFileAsync(new ConvertFileArgs
                        {
                            InputFile = new FileInfo(inputFile),
                            Mapper = mapper,
                            OutputFolder = o.OutputFolder
                        });
                        if (!string.IsNullOrWhiteSpace(outputFile))
                        {
                            Console.WriteLine($"Original file {inputFile}, Converted file {outputFile}");
                        }                        
                    }
                });
        }
    }
}