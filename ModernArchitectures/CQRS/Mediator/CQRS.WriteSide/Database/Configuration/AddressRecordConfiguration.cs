using CQRS.Model.WriteModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS.WriteSide.Database.Configuration
{
    public class AddressRecordConfiguration: IEntityTypeConfiguration<AddressRecord>
    {
        public void Configure(EntityTypeBuilder<AddressRecord> builder)
        {
            builder.ToTable("Address");
        }
    }
}