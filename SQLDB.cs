using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace Invoice365
{
    public interface SQLDB
    {
          void close();
          void connect();
          int ExecuteNonQuery(String sql);
          DbDataReader ExecuteQuery(String sql);
    }
}
