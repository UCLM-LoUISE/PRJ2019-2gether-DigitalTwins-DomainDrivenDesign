﻿using System;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using ChildHDT.Domain.DomainServices;
using ChildHDT.Domain.Entities;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ChildHDT.Infrastructure.InfrastructureServices
{
    public class Messaging : IMessaging
    {
        private IConnection connection;
        private IModel channel;


        public Messaging(string hostname)
        {
            var factory = new ConnectionFactory() { HostName =  hostname};
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }

        public Task Publish(Guid publisherId, string queueInfo, string message)
        {
            var queue = publisherId + "/" + queueInfo;

            channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: queue,
                                 basicProperties: null,
                                 body: body);

            return Task.CompletedTask;
        }

        public Task Publish(Guid publisherId, string message)
        {
            var queue = publisherId.ToString();

            channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: queue,
                                 basicProperties: null,
                                 body: body);

            return Task.CompletedTask;
        }

        public async Task Subscribe(Guid publisherId, string queueInfo)
        {
            var queue = publisherId + "/" + queueInfo;

            channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
            };
            channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);

            await Task.CompletedTask;
        }
    }
}
