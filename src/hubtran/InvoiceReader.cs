using System;
using System.Collections.Generic;
using System.Linq;

namespace hubtran
{
    public class InvoiceReader
    {
        public static string ParseInvoice(string rawText)
        {
            // Take the raw text entry of the chars field and split it up on spaces and :
            // Take last element because I am assuming the invoice number is at the end of the entry
            var chars = rawText.Split(new[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList()
                .Last()
                .ToCharArray();
            
            // Reverse the invoice characters
            Array.Reverse(chars);
            // Use a stack to look at each character since it is reversed last in/first out when
            // need to put it back together
            var currentInvoice = new Stack<char>();
            foreach (var currentChar in chars)
            {
                if (!char.IsDigit(currentChar)) // Right now only looking for digits
                {
                    break;
                }
                currentInvoice.Push(currentChar);
            }
            // If the size of the stack is the same length as the original entry to parse
            // then it is a complete match
            if (currentInvoice.Count != chars.Length)
            {
                currentInvoice.Clear();
            }
            // turn the stack back into it's original string
            var invoice = string.Join("", currentInvoice.ToArray());
            return !string.IsNullOrWhiteSpace(invoice) ? invoice : "";
        }        
    }
}