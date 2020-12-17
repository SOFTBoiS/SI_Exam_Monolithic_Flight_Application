using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using System.Net;
using System.IO;
using Confluent.Kafka.Admin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SI_Exam_Monolithic_Flight_Application.KafkaConnect
{
    public class SearchHistory
    {
        public static async Task<ClientConfig> LoadConfig()
        {
            try
            {


                var config = new ProducerConfig
                {
                    BootstrapServers = "localhost:9092",
                    ClientId = Dns.GetHostName()
                };

                return config;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong creating the config. ", e.Message);
                System.Environment.Exit(1);
                return null; // avoid not-all-paths-return-value compiler error.
            }
        }

        public static async Task CreateTopicMaybe(string name, int numPartitions, short replicationFactor, ClientConfig cloudConfig)
        {
            using (var adminClient = new AdminClientBuilder(cloudConfig).Build())
            {
                try
                {
                    await adminClient.CreateTopicsAsync(new List<TopicSpecification> {
                        new TopicSpecification { Name = name, NumPartitions = numPartitions, ReplicationFactor = replicationFactor } });
                }
                catch (CreateTopicsException e)
                {
                    if (e.Results[0].Error.Code != ErrorCode.TopicAlreadyExists)
                    {
                        Console.WriteLine($"An error occured creating topic {name}: {e.Results[0].Error.Reason}");
                    }
                    else
                    {
                        Console.WriteLine("Topic already exists");
                    }
                }
            }
        }

        public static void Produce(string key, string message, string topic, ClientConfig config)
        {
            using (var producer = new ProducerBuilder<string, string>(config).Build())
            {
                Console.WriteLine($"Producing record: {key} {message}");

                producer.Produce(topic, new Message<string, string> { Key = key, Value = message },
                    (deliveryReport) =>
                    {
                        if (deliveryReport.Error.Code != ErrorCode.NoError)
                        {
                            Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                        }
                        else
                        {
                            Console.WriteLine($"Produced message to: {deliveryReport.TopicPartitionOffset}");
                        }
                    });

                producer.Flush(TimeSpan.FromSeconds(10));

                //Console.WriteLine($"Message was produced to topic {topic}");
            }
        }

        internal static void PrintUsage()
        {
            Console.WriteLine("usage: .. produce|consume <topic> <configPath> [<certDir>]");
            System.Environment.Exit(1);
        }
    }
}
