using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChildHDT.Infrastructure.InfrastructureServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChildHDT.Domain.Entities;
using MQTTnet.Client;

namespace ChildHDT.Infrastructure.InfrastructureServices.Tests
{
    [TestClass()]
    public class MessagingTests
    {
        [TestMethod()]
        public async Task SubscribeTestAsync()
        {
            // ARRANGE
            var messaging = new Messaging();
            var subscriber = new Child(name: "Subscriber", surname: "Unit", age: 12, classroom: "3ºA");

            // ACT
            await messaging.Subscribe(subscriber, "test/topic");

            // ASSERT

        }

        [TestMethod()]
        public async Task PublishTestAsync()
        {
            // ARRANGE
            var messaging = new Messaging();
            var publisher = new Child(name: "Publisher", surname: "Test", age: 10, classroom: "1ºA");

            // ACT
            await messaging.Publish(publisher, "test/topic", "TEST MESSAGE");

            // ASSERT

        }
    }
}