using System.Collections.Generic;

namespace CQRS.Model.WriteModel
{
    public class PersonRecord
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<AddressRecord> Addresses { get; set; }
    }
}