using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChildHDT.Domain.Entities;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using MQTTnet.Server;

namespace ChildHDT.Infrastructure.InfrastructureServices
{
    public class Messaging
    {
        private IMqttClient mqttClient;
        // METHODS

        public Messaging() 
        {
            mqttClient = new MqttFactory().CreateMqttClient();
        }

        public async Task Publish(Child publisher, string queue, string message)
        {
            var topic = publisher.Name + "/" + queue;

            var options = new MqttClientOptionsBuilder()
                .WithClientId(publisher.Id.ToString())
                .WithTcpServer("localhost", 1883)
                .Build();

            var connectionResult = await mqttClient.ConnectAsync(options);

            if (!connectionResult.Equals(MqttClientConnectResultCode.Success)) { 
                //ERROR
            }

            var mqttMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(message)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .WithRetainFlag()
                .Build();

            await mqttClient.PublishAsync(mqttMessage);
        }

        public async Task Subscribe(Child subscriber, string queue) 
        {
            var topic = subscriber.Name + "/" + queue;

            var options = new MqttClientOptionsBuilder()
                .WithClientId(subscriber.Id.ToString())
                .WithTcpServer("localhost", 1883)
                .Build();

            var connectionResult = await mqttClient.ConnectAsync(options);

            if (!connectionResult.Equals(MqttClientConnectResultCode.Success))
            {
                //ERROR
            }

            await mqttClient.SubscribeAsync(topic);

            mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                Console.WriteLine($"Received message: {Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment)}");
                return Task.CompletedTask;
            };

        }
    }
}
