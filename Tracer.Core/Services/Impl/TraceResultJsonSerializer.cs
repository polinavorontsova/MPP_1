using System.Text.Json;
using Tracer.Core.Entities;

namespace Tracer.Core.Services.Impl
{
    public class TraceResultJsonSerializer : ISerializer<TraceResult>
    {
        public string Serialize(TraceResult data)
        {
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                IgnoreNullValues = true,
                WriteIndented = true
            };
            return JsonSerializer.Serialize(data, options);
        }
    }
}