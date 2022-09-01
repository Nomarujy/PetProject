using Confluent.Kafka;
using System.Net;

namespace Portfolio.Areas.HelloKafka.Services.Kafka
{
    public class KafkaProducer : IDisposable
    {
        private readonly IProducer<string, string> _producer;

        public KafkaProducer()
        {
            ProducerConfig cfg = new()
            {
                BootstrapServers = Environment.GetEnvironmentVariable("KAFKA_HOST") ?? "localhost",
                ClientId = Dns.GetHostName(),
            };

            _producer = new ProducerBuilder<string, string>(cfg).Build();
        }

        public void SendMessageAsync(Message<string, string> message, string topic)
        {
            _producer.ProduceAsync(topic, message);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                _producer.Dispose();
            }
            disposed = true;
        }
    }
}
