using ChildHDT.Infrastructure.EventSourcing.Events;
using MQTTnet.Client;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChildHDT.Domain.Entities;

namespace ChildHDT.Infrastructure.EventSourcing.Registries
{
    public abstract class EventStore<T> where T : Event
    {
        protected List<T> Events { get; set; } = new List<T>();
        private readonly IMqttClient _client;
        private readonly string _topic;

        protected EventStore (Child child, String topic)
        {
            _topic = "" + child.Name + "/" + topic;
            var factory = new MqttFactory();
            _client = factory.CreateMqttClient();
        }

        public async Task Start()
        {
            var options = new MqttClientOptionsBuilder()
                .WithClientId("ChildClient")
                .WithTcpServer("192.168.0.102", 1883)
                .WithCredentials("admin", "public")
                .WithCleanSession()
                .Build();

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
