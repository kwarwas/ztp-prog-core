﻿using System.Collections.Generic;

namespace CQRS.ReadSide.Database
{
    public class PersonDetailsRecord
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<AddressRecord> Addresses { get; set; }
    }
}