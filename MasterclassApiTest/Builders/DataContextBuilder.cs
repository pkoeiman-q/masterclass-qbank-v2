using MasterclassApiTest.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace MasterclassApiTest.Builders
{
    public class DataContextBuilder
    {
        private bool _inMemory = true;
        public DataContextBuilder UseInMemory()
        {
            _inMemory = true;
            return this;
        }

        public DataContextBuilder UseSqlite()
        {
            _inMemory = false;
            return this;
        }

        public DataContext Build()
        {
            var options = _inMemory ? BuildInMemoryOptions() : BuildSqliteOptions();
            var dbContext = new DataContext(options);
            dbContext.Database.EnsureCreated();
            return dbContext;
        }

        private DbContextOptions<DataContext> BuildInMemoryOptions()
        {
            return new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Test_database")
                .Options;
        }

        private DbContextOptions<DataContext> BuildSqliteOptions()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            return new DbContextOptionsBuilder<DataContext>()
                .UseSqlite(connection)
                .Options;
        }
    }
}
