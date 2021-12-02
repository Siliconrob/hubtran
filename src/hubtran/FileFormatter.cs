using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace hubtran
{
    public class FileFormatter
    {
        public const string InvoiceKey = "invoice";
        public static async Task<string> ConvertFileAsync(ConvertFileArgs args)
        {
            var textContents = await File.ReadAllTextAsync(args.InputFile.FullName);
            // Convert the text of the file into JSON according to the DivInput class
            // Extract output uses a mapper DivInput -> DivOutput by default
            // search for the Invoice key and pass in a func to extract the invoice
            var outputElements = JsonConvert.DeserializeObject<List<DivInput>>(textContents)
                .Select(input => input.ExtractOutput(args.Mapper, InvoiceKey, InvoiceReader.ParseInvoice))
                .ToList();
            if (outputElements.Count <= 0)
            {
                return "";
            }
            // Take the output elements and format as a nice humany readable indented JSON file
            var outputText = JsonConvert.SerializeObject(outputElements, Formatting.Indented);
            var outputFile = Path.Combine(args.OutputFolder, args.InputFile.Name);
            await File.WriteAllTextAsync(outputFile, outputText);
            return outputFile;
        }        
    }
}