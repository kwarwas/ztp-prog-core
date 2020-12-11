using System;

namespace Messages
{
    public interface IOrderResponse
    {
        string Name { get; set; }
        DateTime HandledDate { get; set; }
    }
}