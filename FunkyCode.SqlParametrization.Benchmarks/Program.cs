using BenchmarkDotNet.Running;
using System;

namespace FunkyCode.SqlParametrization.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Benchmarks>();
        }
    }
}
