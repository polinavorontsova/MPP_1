using System;

namespace Tracer.Core.Services.Impl
{
    public class ConsolePrinter : IPrinter
    {
        public void Print(string data)
        {
            Console.WriteLine(data);
        }
    }
}