using System.Threading;
using Tracer.Services;

namespace ConsoleApplication
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