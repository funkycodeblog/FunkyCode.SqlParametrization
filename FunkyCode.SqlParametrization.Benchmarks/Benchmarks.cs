using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace FunkyCode.SqlParametrization.Benchmarks
{
    [MemoryDiagnoser]
    public class Benchmarks
    {
        List<DateTime> _dates = new List<DateTime>();

        public Benchmarks()
        {
            // InitializeDates();
        }

        //public void InitializeDates()
        //{
        //    var rnd = new Random(DateTime.Now.Millisecond);
        //    var startDate = new DateTime(2013, 07, 31);

        //    for (int i = 0; i < 1000; i++)
        //    {
        //        var days = rnd.Next(1, 368);
        //        var dt = startDate.AddDays(days);
        //        _dates.Add(dt);
        //    }
        //}

        [Benchmark]
        public void ByDynamicSql()
        {
            //var service = new DynamicSqlService();
            //var avg = service.GetAverage(_dates);
        }

        [Benchmark]
        public void ByParametrizedSql()
        {
            //var service = new ParametrizedSqlService();
            //var avg = service.GetAverage(_dates);

        }
    
    }
}
