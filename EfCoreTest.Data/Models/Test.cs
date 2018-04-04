using System;

namespace EfCoreTest.Data.Models
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
    
    public interface ISoftDeletable
    {
        DateTime? DeletedOn { get; set; }
    }
    
    public class Test: ISoftDeletable, IEntity
    {
        public Guid Id { get; set; }
        public DateTime? DeletedOn { get; set; }
        
        public string Value { get; set; }
    }
}