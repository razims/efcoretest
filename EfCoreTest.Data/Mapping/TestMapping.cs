using EfCoreTest.Data.Models;
using EfCoreTest.Data.Providers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreTest.Data.Mapping
{
    public class TestMapping: BaseEntityTypeConfiguration<Test>
    {
        public TestMapping(IDateTimeProvider dateTimeProvider) : base(dateTimeProvider)
        {
        }

        public override void Configure(EntityTypeBuilder<Test> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Value).IsRequired(false);
        }
    }
}