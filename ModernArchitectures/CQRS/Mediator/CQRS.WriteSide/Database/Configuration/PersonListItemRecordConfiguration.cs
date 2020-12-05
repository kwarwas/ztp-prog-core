using CQRS.Model.ReadModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS.WriteSide.Database.Configuration
{
    public class PersonListItemRecordConfiguration: IEntityTypeConfiguration<PersonListItemRecord>
    {
        public void Configure(EntityTypeBuilder<PersonListItemRecord> builder)
        {
            builder
                .ToTable("PersonList")
                .HasIndex(x => x.OriginalId);
        }
    }
}