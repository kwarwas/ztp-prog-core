namespace Messages
{
    public class SubmitOrderResponse
    {
        public string Message { get; }

        public SubmitOrderResponse(string message)
        {
            Message = message;
        }
    }
}