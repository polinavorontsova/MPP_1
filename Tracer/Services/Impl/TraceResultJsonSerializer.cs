using System.Text.Json;
using Tracer.Entities;

namespace Tracer.Services.Impl
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