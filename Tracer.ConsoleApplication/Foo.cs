using System.Threading;
using Tracer.Core.Services;

namespace Tracer.ConsoleApplication
{
    public class Foo
    {
        private readonly ITracer _tracer;

        public Foo(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void Method()
        {
            _tracer.StartTrace();
            Thread.Sleep(200);
            _tracer.StopTrace();
        }
    }
}