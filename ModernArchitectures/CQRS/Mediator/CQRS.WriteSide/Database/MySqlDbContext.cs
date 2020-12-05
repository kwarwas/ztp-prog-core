using CQRS.Model.ReadModel;
using CQRS.Model.WriteModel;
using CQRS.WriteSide.Database.Configuration;
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
            optionsBuilder.UseMySql(@"Server=localhost;port=3307;database=people;uid=root;pwd=password;");
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