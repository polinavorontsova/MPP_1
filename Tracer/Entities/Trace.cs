using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Tracer.Entities
{
    [XmlType("thread")]
    public class Trace
    {
        public Trace()
        {
        }

        public Trace(int id)
        {
            Id = id;
        }

        [JsonPropertyName("id")]
        [XmlAttribute("id")]
        public int Id { get; set; }

        [JsonPropertyName("time")]
        [XmlAttribute("time")]
        public long Time { get; set; }

        [JsonPropertyName("methods")]
        [XmlElement("method")]
        public List<Method> Methods { get; } = new();

        [JsonIgnore] [XmlIgnore] public Stack<Method> MethodsStack { get; } = new();

        public void AddMethod(Method method, IList<Method> rootMethods = null)
        {
            rootMethods ??= Methods;

            foreach (var currentMethod in rootMethods)
            {
                var methodStackTracePrefix = method.StackTracePrefix;
                var currentMethodStackTracePrefix = currentMethod.StackTracePrefix;

                if (methodStackTracePrefix.Equals(currentMethodStackTracePrefix))
                {
                    PushMethod(method, rootMethods);
                    return;
                }

                if (methodStackTracePrefix.EndsWith(currentMethodStackTracePrefix))
                {
                    AddMethod(method, currentMethod.Methods);
                    return;
                }
            }

            PushMethod(method, rootMethods);
        }

        private void PushMethod(Method method, IList<Method> rootMethods)
        {
            rootMethods.Insert(0, method);
            MethodsStack.Push(method);
            method.StartTimer();
        }

        public void DeleteMethod(Method method)
        {
            if (MethodsStack.Count == 0) throw new Exception($"Redundant stopTrace() call for {method}.");

            var lastMethod = MethodsStack.Pop();

            if (!lastMethod.Equals(method))
                throw new Exception($"Invalid method pair: expected - {lastMethod}, got - {method}.");

            lastMethod.StopTimer();
        }
    }
}