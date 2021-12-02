using System;
using System.Collections.Generic;
using AutoMapper;

namespace hubtran
{
    public class DivInput
    {
        public double left { get; set; }
        public double top { get; set; }
        public double width { get; set; }
        public double height { get; set; }
        public string chars { get; set; }         
    }

    public class DivOutput
    {
        public double left { get; set; }
        public double top { get; set; }
        public double width { get; set; }
        public double height { get; set; }
        public object chars { get; set; }          
    }

    public static class DivInputExtensions
    {
        public static DivOutput ExtractOutput(this DivInput input, IMapper mapper, string invoiceKey, Func<string, string> parseInvoiceFn)
        {
            var output = mapper.Map<DivOutput>(input);
            // If the chars element contains the case insensitive text of `invoice`
            // then we try to parse with the provided func
            if (!input.chars.Contains(invoiceKey, StringComparison.OrdinalIgnoreCase))
            {
                return output;
            }
            // If you give me a null func then default to empty parse so it 
            // writes the same input as output
            parseInvoiceFn ??= s => "";
            var extractedInvoice = parseInvoiceFn(input.chars);
            output.chars = input.chars;
            // If can't extract an invoice from the parse func then
            // write the input chars as output
            if (!string.IsNullOrWhiteSpace(extractedInvoice))
            {
                // Able to parse the invoice out according to parse func so write a consistent key
                // in the array I am using `invoice` as all the other keys are lowercase
                output.chars = new List<string> { invoiceKey, extractedInvoice };
            }
            return output;
        }        
    }
    
}