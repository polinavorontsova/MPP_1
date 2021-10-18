using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Tracer.Entities
{
    public class Trace
    {
        public Trace()
        {
        }

        public Trace(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public long Time { get; set; }
        public List<Method> Methods { get; } = new();
        public Stack<Method> MethodsStack { get; } = new();

        public void AddMethod(Method method, IList<Method> rootMethods = null)
        {
            throw new NotImplementedException();

        }

        private void PushMethod(Method method, IList<Method> rootMethods)
        {
            throw new NotImplementedException();
        }

        public void DeleteMethod(Method method)
        {
            throw new NotImplementedException();
        }
    }
}