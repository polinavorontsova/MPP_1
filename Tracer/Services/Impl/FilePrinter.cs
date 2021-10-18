using System.IO;
using System.Text;

namespace Tracer.Services.Impl
{
    public class FilePrinter : IPrinter
    {
        private readonly string _filePath;

        public FilePrinter(string filePath)
        {
            _filePath = filePath;
        }

        public void Print(string data)
        {
            File.WriteAllText(_filePath, data, Encoding.UTF8);
        }
    }
}