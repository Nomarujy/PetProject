using Confluent.Kafka;

namespace MessageConsumer
{
    public class ConsumerService
    {
        private readonly IConsumer<string, string> consumer;

        public ConsumerService()
        {
            ConsumerConfig cfg = new()
            {
                BootstrapServers = "192.168.1.200:29000",
                EnableAutoCommit = false,
                GroupId = "ChatGroup",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            consumer = new ConsumerBuilder<string, string>(cfg).Build();

            consumer.Subscribe("FastChat");
        }

        public void ConsumeLoop(Action<Message<string, string>> action, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var res = consumer.Consume(token);

                if (res != null)
                {
                    action(res.Message);
                    consumer.Commit(res);
                }
            }
        }
    }
}