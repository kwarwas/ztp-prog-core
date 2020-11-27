using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS.WriteSide.Database.WriteModel
{
    public enum AddressType
    {
        Main,
        Additional
    }
    
    public class AddressRecord
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public AddressType AddressType { get; set; }
        public int PersonId { get; set; }
        public PersonRecord Person { get; set; }
    }
    
    public class AddressRecordConfiguration: IEntityTypeConfiguration<AddressRecord>
    {
        public void Configure(EntityTypeBuilder<AddressRecord> builder)
        {
            builder.ToTable("Address");
        }
    }
}