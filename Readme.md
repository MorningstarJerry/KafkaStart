# Kafka

https://www.cnblogs.com/linjiqin/p/13196347.html

https://kafka.apachecn.org/quickstart.html



```
wget https://mirrors.bfsu.edu.cn/apache/kafka/2.8.0/kafka_2.12-2.8.0.tgz

yum list java

/usr/lib/jvm

/usr/mysoft/kafka_2.12-2.8.0
bin/zookeeper-server-start.sh  config/zookeeper.properties

[root@JabilServer001 kafka_2.12-2.8.0]# bin/zookeeper-server-start.sh  config/zookeeper.properties 

静默启动 kafka
nohup bin/zookeeper-server-start.sh config/zookeeper.properties &
nohup bin/kafka-server-start.sh config/server.properties &

bin/zookeeper-server-start.sh -daemon config/zookeeper.properties &
bin/kafka-server-start.sh -daemon config/server.properties &

```

# 项目地址
C:\Users\2294765\source\repos\KafkaStart

# 发布者
```
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

```

# 订阅者
```
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
```

# 通过命令行直接在终端查看
![image-20210702162445658](C:\Users\2294765\AppData\Roaming\Typora\typora-user-images\image-20210702162445658.png)


# 通过.Net Core Customer 订阅查看
![image-20210702162957500](C:\Users\2294765\AppData\Roaming\Typora\typora-user-images\image-20210702162957500.png)

# Java

```
/usr/lib/jvm
```

