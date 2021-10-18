using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Tracer.Core.Entities;
using Trace = Tracer.Core.Entities.Trace;

namespace Tracer.Core.Services.Impl
{
    public class MethodTracer : ITracer
    {
        private const int CallingMethodStackFrameNumber = 2;
        private const string StackTraceDelimiter = "\r\n";

        private readonly TraceResult _traceResult = new();

        public void StartTrace()
        {
            var currentTrace = GetCurrentExecutingTrace();
            var currentMethod = GetCurrentExecutingMethod();
            currentTrace.AddMethod(currentMethod);
        }

        public void StopTrace()
        {
            var currentTrace = GetCurrentExecutingTrace();
            var currentMethod = GetCurrentExecutingMethod();
            currentTrace.DeleteMethod(currentMethod);
        }

        public TraceResult GetTraceResult()
        {
            foreach (var trace in _traceResult.Traces)
            {
                if (trace.MethodsStack.Count != 0)
                    throw new Exception($"The tracing wasn't stopped for the thread with id: {trace.Id}");

                trace.Time = trace.Methods.Select(method => method.Time).Sum();
            }

            return _traceResult;
        }

        private Trace GetCurrentExecutingTrace()
        {
            var currentThreadId = Thread.CurrentThread.ManagedThreadId;
            return _traceResult.GetTrace(currentThreadId);
        }

        private Method GetCurrentExecutingMethod()
        {
            var stackTrace = new StackTrace();
            var currentExecutingMethodBase = stackTrace.GetFrame(CallingMethodStackFrameNumber)?.GetMethod();
            var methodName = currentExecutingMethodBase?.Name;
            var methodClass = currentExecutingMethodBase?.ReflectedType?.Name;
            var stackTraceArray = stackTrace.ToString().Trim().Split(StackTraceDelimiter);
            var stackTracePrefix = string.Join(StackTraceDelimiter, stackTraceArray, CallingMethodStackFrameNumber,
                stackTraceArray.Length - CallingMethodStackFrameNumber);
            return new Method(methodName, methodClass, stackTracePrefix);
        }
    }
}