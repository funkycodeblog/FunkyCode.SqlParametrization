using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FunkyCode.SqlParametrization
{
    public class Consts
    {
        public const string ConnectionString = @"Server=(localdb)\MSSQLLocalDB;Initial Catalog = AdventureWorks2016; Integrated Security = True";
    }

    class Program
    {
        static void Main(string[] args)
        {
            var dates = InitializeDates();

            ExecuteTest(new DynamicSqlService(), dates);
            Console.ReadLine();

            ExecuteTest(new ParametrizedSqlService(), dates);
            Console.ReadLine();
        }

        public static void ExecuteTest(IService service, List<Tuple<DateTime, string>> dates)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Console.WriteLine($"Execute test for {service.GetType().Name}");
            Console.WriteLine($"Drop and restore database, then [Enter]");
            Console.ReadLine();
            Console.WriteLine("Start...");
            var avg = service.ExecuteTest(dates);
            stopWatch.Stop();
            Console.WriteLine("Stop.");
            Console.WriteLine($"Count={avg}");
            Console.WriteLine($"Elapsed={stopWatch.ElapsedMilliseconds}");
        }

        public static List<Tuple<DateTime, string>> InitializeDates()
        {
            var dates = new List<Tuple<DateTime, string>>();
            var randomizer = new Random(DateTime.Now.Millisecond);
            var startDate = new DateTime(2013, 07, 31);
            var letter = 'Y';

            for (int i = 0; i < 10000; i++)
            {
                var days = randomizer.Next(1, 368);
                var dt = startDate.AddDays(days);

                if (++letter == 'Z')
                    letter = 'A';

                dates.Add(new Tuple<DateTime, string>(dt, letter.ToString()));
            }

            return dates;
        }
    }
}
