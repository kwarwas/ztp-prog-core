namespace CQRS.Model.WriteModel
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
}