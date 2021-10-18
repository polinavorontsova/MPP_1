using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Tracer.Entities
{
    public class Method
    {
        private readonly Stopwatch _stopWatch = new();

        public Method()
        {
        }

        public Method(string name, string className, string stackTracePrefix)
        {
            Name = name;
            Class = className;
            StackTracePrefix = stackTracePrefix;
        }

        [JsonPropertyName("name")]
        [XmlAttribute("name")]
        public string Name { get; set; }

        [JsonPropertyName("class")]
        [XmlAttribute("class")]
        public string Class { get; set; }

        [JsonPropertyName("time")]
        [XmlAttribute("time")]
        public long Time { get; set; }

        [JsonPropertyName("methods")]
        [XmlElement("method")]
        public List<Method> Methods { get; } = new();

        [JsonIgnore] [XmlIgnore] public string StackTracePrefix { get; }

        public void StopTimer()
        {
            _stopWatch.Stop();
            Time = _stopWatch.ElapsedMilliseconds;
        }

        public void StartTimer()
        {
            _stopWatch.Start();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            var method = (Method) obj;
            return StackTracePrefix != null && StackTracePrefix.Equals(method.StackTracePrefix);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StackTracePrefix);
        }

        public override string ToString()
        {
            return $"Method{{name={Name}, class={Class}}}";
        }
    }
}