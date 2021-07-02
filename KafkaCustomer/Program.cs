using Confluent.Kafka;
using System;
using System.Threading;

namespace KafkaCustomer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World kafka consumer !");

            var config = new ConsumerConfig
            {
                BootstrapServers = "8.135.118.30:9092",
                GroupId = "foo",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            var cancel = false;

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                var topic = "test";
                consumer.Subscribe(topic);

                while (!cancel)
                {
                    var consumeResult = consumer.Consume(CancellationToken.None);

                    Console.WriteLine($"Consumer message: { consumeResult.Message.Value} topic: {consumeResult.Topic} Partition: {consumeResult.Partition}");
                }

                consumer.Close();
            }
        }
    }
}
