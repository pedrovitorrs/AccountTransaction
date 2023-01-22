using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountTransaction.WebAPI.Core.DatabaseFlavor
{
    public enum DatabaseType
    {
        None,
        SqlServer,
        MySql,
        Postgre,
        Sqlite,
    }
}
