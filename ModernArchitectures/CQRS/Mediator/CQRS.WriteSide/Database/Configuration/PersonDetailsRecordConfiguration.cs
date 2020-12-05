using CQRS.Model.ReadModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS.WriteSide.Database.Configuration
{
    public class PersonDetailsRecordConfiguration: IEntityTypeConfiguration<PersonDetailsRecord>
    {
        public void Configure(EntityTypeBuilder<PersonDetailsRecord> builder)
        {
            builder.ToTable("PersonDetails");
        }
    }
}