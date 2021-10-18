using System;

namespace Tracer.Services.Impl
{
    public class ConsolePrinter : IPrinter
    {
        public void Print(string data)
        {
            Console.WriteLine(data);
        }
    }
}