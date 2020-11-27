using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS.WriteSide.Database.WriteModel
{
    public class PersonRecord
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<AddressRecord> Addresses { get; set; }
    }
    
    public class PersonRecordConfiguration: IEntityTypeConfiguration<PersonRecord>
    {
        public void Configure(EntityTypeBuilder<PersonRecord> builder)
        {
            builder.ToTable("Person");
        }
    }
}