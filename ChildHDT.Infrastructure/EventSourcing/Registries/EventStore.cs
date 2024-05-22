using ChildHDT.Domain.Entities;
using Microsoft.Extensions.Configuration;
using ChildHDT.Infrastructure.EventSourcing.Events;
using MQTTnet.Client;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHDT.Infrastructure.EventSourcing.Registries
{
    public abstract class EventStore<T> where T : Event
    {
        protected List<T> Events { get; set; } = new List<T>();
        private readonly IMqttClient _client;
        private readonly string _topic;

        protected EventStore (Child child, string topic, IConfiguration configuration)
        {
            _topic = "" + child.Name + "/" + topic;
            var factory = new MqttFactory();
            _client = factory.CreateMqttClient();

            var mqttServer = configuration["MQTT:Server"];
            var mqttPort = int.Parse(configuration["MQTT:Port"]);
            var mqttUserName = configuration["MQTT:UserName"];
            var mqttPassword = configuration["MQTT:Password"];

            var options = new MqttClientOptionsBuilder()
                .WithClientId("ChildClient")
                .WithTcpServer(mqttServer, mqttPort)
                .WithCredentials(mqttUserName, mqttPassword)
                .WithCleanSession()
                .Build();

            Start(options).Wait();
        }

        public async Task Start(MqttClientOptions options)
        {

            _client.ApplicationMessageReceivedAsync += e =>
            {
                var message = e.ApplicationMessage;
                var payload = Encoding.UTF8.GetString(message.PayloadSegment);
                var eventData = DeserializeEvent(payload);
                ReceiveEvent(eventData);
                return Task.CompletedTask;
            };

            await _client.ConnectAsync(options);
            await _client.SubscribeAsync(_topic);

        }

        public void ReceiveEvent(T eventData)
        {
            Events.Add(eventData);
        }

        public List<T> GetEvents()
        {
            return Events;
        }

        protected abstract T DeserializeEvent(string payload);
    }
}
