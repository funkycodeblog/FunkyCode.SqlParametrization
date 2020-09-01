using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FunkyCode.SqlParametrization
{
    public class ParametrizedSqlService : IService
    {
        public int ExecuteTest(List<Tuple<DateTime, string>> dateTimes)
        {
            var rowCounts = new ConcurrentBag<int>();

            var sql = @$"SELECT count(TransactionId) FROM [Production].[TransactionHistory] H
                         JOIN Production.Product P ON P.ProductID = H.ProductID
                         JOIN Production.ProductModel M ON M.ProductModelID = P.ProductModelID
                         WHERE H.TransactionDate > @date AND M.Name LIKE @letter";

            Parallel.ForEach(dateTimes, (dt) =>
            {
                using var conn = new SqlConnection(Consts.ConnectionString);
                conn.Open();

                using var cmd = new SqlCommand(sql, conn) { CommandType = CommandType.Text };
                cmd.Parameters.Add(new SqlParameter("@date", dt.Item1));
                cmd.Parameters.Add(new SqlParameter("@letter", $"{dt.Item2}%"));

                var rowCount = (int)cmd.ExecuteScalar();
                rowCounts.Add(rowCount);
            });

            var sum = rowCounts.Sum();
            return sum;
        }
    }
}