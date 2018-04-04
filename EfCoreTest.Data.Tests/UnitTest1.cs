using System;
using System.Linq;
using EfCoreTest.Data.Models;
using EfCoreTest.Data.Providers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace EfCoreTest.Data.Tests
{
    public class UnitTest
    {
        private readonly Mock<IDateTimeProvider> dateTimeProvider;
        private readonly DbContext dbContext;

        private DbSet<Test> Table => dbContext.Set<Test>();
        private int SaveChanges() => dbContext.SaveChanges();

        private void SetTime(DateTime dateTime)
        {
            dateTimeProvider.SetupGet(x => x.UtcNow).Returns(()=>
            {
                // used for breakpoint
                return dateTime;
            });
        }

        public UnitTest()
        {
            dateTimeProvider = new Mock<IDateTimeProvider>();
            
            dbContext = new SqlLiteDbContext(dateTimeProvider.Object);
            dbContext.Database.EnsureCreated();
        }
        
        [Fact]
        public void Test1()
        {
            // setup time provider
            SetTime(DateTime.UtcNow);
            
            // table is empty
            Table.AsNoTracking().Count().Should().Be(0);
            
            // add one entry
            var entry = new Test();
            Table.Add(entry);
            SaveChanges();
            
            // table has one entry
            Table.AsNoTracking().Count().Should().Be(1);
            
            // mark as deleted
            entry.DeletedOn = DateTime.UtcNow.AddMinutes(-1);
            Table.Update(entry);
            SaveChanges();
            
            // table has no entries
            Table.AsNoTracking().Count().Should().Be(0);
            
            // rewind time provider
            SetTime(DateTime.UtcNow.AddMinutes(-2));
            
            // table should have 1 entry, but has no entries
            // goto file <SqlLiteDbContext.cs> in EfCoreTest.Data
            // line 35 uncomment it
            // rerun the test
            Table.AsNoTracking().Count().Should().Be(1);
        }
    }
}