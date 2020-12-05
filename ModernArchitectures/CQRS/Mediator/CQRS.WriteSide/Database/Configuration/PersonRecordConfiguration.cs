using CQRS.Model.WriteModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS.WriteSide.Database.Configuration
{
    public class PersonRecordConfiguration: IEntityTypeConfiguration<PersonRecord>
    {
        public void Configure(EntityTypeBuilder<PersonRecord> builder)
        {
            builder.ToTable("Person");
        }
    }
}