using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace Tracer.Entities
{
    public class TraceResult
    {
        private readonly ConcurrentDictionary<int, Trace> _tracesDictionary = new();

        public IEnumerable<Trace> Traces => _tracesDictionary.Values.ToImmutableList();

        public Trace GetTrace(int id)
        {
            return _tracesDictionary.GetOrAdd(id, new Trace(id));
        }
    }
}