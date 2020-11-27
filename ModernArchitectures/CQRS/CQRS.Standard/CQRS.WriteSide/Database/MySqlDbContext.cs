using CQRS.WriteSide.Database.ReadModel;
using CQRS.WriteSide.Database.WriteModel;
using Microsoft.EntityFrameworkCore;

namespace CQRS.WriteSide.Database
{
    public class MySqlDbContext : DbContext
    {
        public DbSet<PersonRecord> People { get; set; }
        public DbSet<AddressRecord> Addresses { get; set; }
        
        public DbSet<PersonListItemRecord> PeopleList { get; set; }
        public DbSet<PersonDetailsRecord> PeopleDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(@"Server=localhost;database=people;uid=root;pwd=password;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonRecordConfiguration());
            modelBuilder.ApplyConfiguration(new AddressRecordConfiguration());
            
            modelBuilder.ApplyConfiguration(new PersonListItemRecordConfiguration());
            modelBuilder.ApplyConfiguration(new PersonDetailsRecordConfiguration());
        }
    }
}