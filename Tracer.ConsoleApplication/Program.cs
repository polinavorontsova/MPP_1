using System.Threading;
using Tracer.Core.Entities;
using Tracer.Core.Services;
using Tracer.Core.Services.Impl;

namespace Tracer.ConsoleApplication
{
    internal class Program
    {
        private static readonly ITracer Tracer = new MethodTracer();

        private static void Main()
        {
            new Program().Inner();
            new Program().MoreInsideInner();

            var thread = new Thread(OtherThread);
            thread.Start();
            thread.Join();

            var traceResult = Tracer.GetTraceResult();

            ISerializer<TraceResult> jsonSerializer = new TraceResultJsonSerializer();
            ISerializer<TraceResult> xmlSerializer = new TraceResultXmlSerializer();
            var xmlResult = xmlSerializer.Serialize(traceResult);
            var jsonResult = jsonSerializer.Serialize(traceResult);

            IPrinter jsonPrinter = new FilePrinter("../../result.json");
            IPrinter xmlPrinter = new FilePrinter("../../result.xml");
            IPrinter consolePrinter = new ConsolePrinter();

            xmlPrinter.Print(xmlResult);
            jsonPrinter.Print(jsonResult);
            consolePrinter.Print(xmlResult);
            consolePrinter.Print(jsonResult);
        }

        private void Inner()
        {
            Tracer.StartTrace();
            Thread.Sleep(500);
            InsideInner();
            new Foo(Tracer).Method();
            InsideInner();
            Tracer.StopTrace();
        }

        private void InsideInner()
        {
            Tracer.StartTrace();
            Thread.Sleep(700);
            MoreInsideInner();
            Tracer.StopTrace();
        }

        private void MoreInsideInner()
        {
            Tracer.StartTrace();
            Thread.Sleep(500);
            Tracer.StopTrace();
        }

        private static void OtherThread()
        {
            Tracer.StartTrace();
            Thread.Sleep(500);
            Tracer.StopTrace();
        }
    }
}