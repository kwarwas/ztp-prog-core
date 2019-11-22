namespace Messages
{
    public interface IOrderRequest
    {
        string Name { get; set; }
        double Weight { get; set; }
    }
}