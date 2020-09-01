using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FunkyCode.SqlParametrization
{
    public class DynamicSqlService : IService
    {
        public int ExecuteTest(List<Tuple<DateTime, string>> dateTimes)
        {
            var rowCounts = new ConcurrentBag<int>();

            Parallel.ForEach(dateTimes, (dt) =>
            {
                var dateStr = $"{dt.Item1.Year}{dt.Item1.Month:00}{dt.Item1.Day:00}";

                var sql = @$"SELECT count(TransactionId)  FROM  [Production].[TransactionHistory] H
                             JOIN Production.Product P ON P.ProductID = H.ProductID
                             JOIN Production.ProductModel M ON M.ProductModelID = P.ProductModelID
                             WHERE H.TransactionDate > '{dateStr}' AND M.Name LIKE '{dt.Item2}%'";

                using var conn = new SqlConnection(Consts.ConnectionString);
                conn.Open();

                using var cmd = new SqlCommand(sql, conn) { CommandType = CommandType.Text };

                var rowCount = (int)cmd.ExecuteScalar();
                rowCounts.Add(rowCount);
            });

            var sum = rowCounts.Sum();
            return sum;
        }
    }
}
