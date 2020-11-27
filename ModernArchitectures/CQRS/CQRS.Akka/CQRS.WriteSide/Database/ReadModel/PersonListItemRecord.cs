using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS.WriteSide.Database.ReadModel
{
    public class PersonListItemRecord
    {
        public int Id { get; set; }
        public int OriginalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AddressesCount { get; set; }
    }
    
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