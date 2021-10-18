using Tracer.Core.Entities;

namespace Tracer.Core.Services
{
    public interface ITracer
    {
        void StartTrace();
        void StopTrace();
        TraceResult GetTraceResult();
    }
}