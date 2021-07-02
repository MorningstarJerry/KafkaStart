using Confluent.Kafka;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaStart
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World Producer!");

            var config = new ProducerConfig
            {
                BootstrapServers = "8.135.118.30:9092",
                ClientId = Dns.GetHostName(),
            };


            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                string topic = "test";
                for (int i = 0; i < 100; i++)
                {
                    var msg = "message " + i;
                    Console.WriteLine($"Send message:   value {msg}");
                    var result = await producer.ProduceAsync(topic, new Message<Null, string> { Value = msg });
                    Console.WriteLine($"Result: key {result.Key} value {result.Value} partition:{result.TopicPartition}");
                    Thread.Sleep(500);
                }
            }

            Console.ReadLine();
        }
    }
}
