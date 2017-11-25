using System;
using System.Threading.Tasks;
using ActorStashing.Messages;

namespace ActorStashing
{
    class ExternalServiceSimulator
    {
        private int _counter;

        public bool IsBusy => ++_counter == 2 || _counter == 3;

        public void Proccess(OrderMessage message)
        {
            Console.WriteLine("Message {0} processing", message.Id);
            Task.Delay(TimeSpan.FromSeconds(1)).Wait();
        }
    }
}
