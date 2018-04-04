using System;

namespace EfCoreTest.Data.Providers
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }

    public class DateTimeProvider: IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}