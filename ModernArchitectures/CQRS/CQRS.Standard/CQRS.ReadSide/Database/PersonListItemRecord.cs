namespace CQRS.ReadSide.Database
{
    public class PersonListItemRecord
    {
        public int Id { get; set; }
        public int OriginalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AddressesCount { get; set; }
    }
}