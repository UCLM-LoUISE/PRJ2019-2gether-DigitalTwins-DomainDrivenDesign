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
using System.Net;

namespace ChildHDT.Infrastructure.EventSourcing.Registries
{
    public abstract class EventStore<T> where T : Event
    {
        protected List<T> Events { get; set; } = new List<T>();
        private readonly IMqttClient _client;
        private readonly string _topic;

        protected EventStore (Guid id, string topic, string server, int port, string user, string pwd)
        {
            _topic = "" + id + "/" + topic;
            var factory = new MqttFactory();
            _client = factory.CreateMqttClient();

            var mqttServer = server;
            var mqttPort = port;
            var mqttUserName = user;
            var mqttPassword = pwd;

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

        public T GetLastEvent()
        {
            return Events[Events.Count - 1];
        }

        public List<T> GetEventsBetweenDates(DateTime from, DateTime to)
        {
            if (from > to)
            {
                throw new ArgumentException("Start date cannot be greater than end date");
            }

            return Events.Where(e => e.Timestamp >= from && e.Timestamp <= to).ToList();
        }

        protected abstract T DeserializeEvent(string payload);
    }
}
