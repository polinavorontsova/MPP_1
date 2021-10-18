using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using NUnit.Framework;
using Tracer.Core.Services;
using Tracer.Core.Services.Impl;

namespace Tracer.Tests
{
    public class TracerTests
    {
        private ITracer _tracer;

        [SetUp]
        public void Setup()
        {
            _tracer = new MethodTracer();
        }

        private void SampleMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
        }


        [TestCase(0)]
        [TestCase(1)]
        [TestCase(3)]
        public void MethodTracer_NumberOfTracesIsTheSameAsNumberOfThreads(int threadsCount)
        {
            var threads = new List<Thread>();
            for (var i = 0; i < threadsCount; i++) threads.Add(new Thread(SampleMethod));

            foreach (var thread in threads)
            {
                thread.Start();
                thread.Join();
            }

            Assert.AreEqual(threadsCount, _tracer.GetTraceResult().Traces.ToImmutableList().Count);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public void MethodTracer_NumberOfRootMethodsInTheTraceIsCorrect(int methodsCount)
        {
            for (var i = 0; i < methodsCount; i++) SampleMethod();

            var result = _tracer.GetTraceResult().Traces.ToImmutableList()[0].Methods.Count;
            Assert.AreEqual(methodsCount, result);
        }

        [TestCase(0)]
        [TestCase(100)]
        [TestCase(200)]
        public void MethodTracer_ThreadExecutionTimeIsGreaterOrEqualThanTimeout(int timeout)
        {
            _tracer.StartTrace();
            Thread.Sleep(timeout);
            _tracer.StopTrace();

            var executionTime = _tracer.GetTraceResult().Traces.ToImmutableList()[0].Time;
            Assert.IsTrue(executionTime >= timeout);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        public void TracerImpl_ThreadInnerMethodsCountingIsCorrect(int innerMethodsCount)
        {
            _tracer.StartTrace();
            for (var i = 0; i < innerMethodsCount; i++) SampleMethod();

            _tracer.StopTrace();

            var result = _tracer.GetTraceResult().Traces.ToImmutableList()[0].Methods[0].Methods.Count;
            Assert.AreEqual(innerMethodsCount, result);
        }
    }
}