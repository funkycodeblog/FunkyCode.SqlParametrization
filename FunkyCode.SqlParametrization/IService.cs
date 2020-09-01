using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;

namespace FunkyCode.SqlParametrization
{
    public interface IService
    {
        int ExecuteTest(List<Tuple<DateTime, string>> dateTimes);
    }
}
