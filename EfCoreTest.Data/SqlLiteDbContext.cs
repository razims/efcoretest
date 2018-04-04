using System.Data;
using System.Data.Common;
using EfCoreTest.Data.Infrastructure;
using EfCoreTest.Data.Mapping;
using EfCoreTest.Data.Providers;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace EfCoreTest.Data
{
    public class SqlLiteDbContext: DbContext
    {
        private static readonly LoggerFactory LoggerFactory = new LoggerFactory(new[] {new DebugLoggerProvider((_, __) => true)});
        
        private readonly IDateTimeProvider dateTimeProvider;
        private SqliteConnection connection;

        public SqlLiteDbContext(IDateTimeProvider dateTimeProvider)
        {
            this.dateTimeProvider = dateTimeProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            connection = new SqliteConnection("DataSource=:memory:");

            if (connection.State == ConnectionState.Closed)
                connection.Open();
            
            optionsBuilder.UseSqlite(connection);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseLoggerFactory(LoggerFactory);
            // optionsBuilder.UseMemoryCache(new DummyMemoryCache());
            
            
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TestMapping(dateTimeProvider));
            
            base.OnModelCreating(modelBuilder);
        }

        public override void Dispose()
        {
            connection?.Dispose();
            connection = null;
            
            base.Dispose();
        }
    }
}