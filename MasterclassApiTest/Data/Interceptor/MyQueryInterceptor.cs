using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Diagnostics;

namespace MasterclassApiTest.Data.Interceptor
{
    public class MyQueryInterceptor : DbCommandInterceptor
    {
        // runs before a query is executed
        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            return result;
        }

        // runs after a query is excuted
        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            Debug.WriteLine("\n= = = = = Result = = = = = ");
            Debug.WriteLine(eventData.CommandId);
            Debug.WriteLine(eventData.Duration);
            Debug.WriteLine("= = = = = Done = = = = = \n");
            return result;
        }
    }
}
