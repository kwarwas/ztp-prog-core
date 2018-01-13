using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS.WriteSide.Database.ReadModel
{
    public class PersonDetailsRecord
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    
    public class PersonDetailsRecordConfiguration: IEntityTypeConfiguration<PersonDetailsRecord>
    {
        public void Configure(EntityTypeBuilder<PersonDetailsRecord> builder)
        {
            builder.ToTable("PersonDetails");
        }
    }
}