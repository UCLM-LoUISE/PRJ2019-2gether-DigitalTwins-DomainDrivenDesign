using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChildHDT.Domain.Entities;
using MQTTnet;
using MQTTnet.Client;
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
                .WithTcpServer("localhost")
                .Build();

            await mqttClient.ConnectAsync(options);

            var messageBuilder = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(message)
                .WithRetainFlag();

            var mqttMessage = messageBuilder.Build();
            await mqttClient.PublishAsync(mqttMessage);
        }

        public async Task Subscribe(Child publisher, string queue) 
        {
            var topic = publisher.Name + "/" + queue;

            var options = new MqttClientOptionsBuilder()
                .WithClientId(publisher.Id.ToString())
                .WithTcpServer("localhost")
                .Build();

            await mqttClient.ConnectAsync(options);
            await mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).Build());

        }
    }
}
