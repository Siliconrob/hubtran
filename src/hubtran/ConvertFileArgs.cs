using System.IO;
using AutoMapper;

namespace hubtran
{
    public class ConvertFileArgs
    {
        public string OutputFolder { get; set; }
        public IMapper Mapper { get; set; }
        public FileInfo InputFile { get; set; }        
    }
}