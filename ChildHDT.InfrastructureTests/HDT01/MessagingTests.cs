using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChildHDT.Infrastructure.InfrastructureServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTnet.Client;
using ChildHDT.Domain.ValueObjects;
using ChildHDT.API.ApplicationServices;
using ChildHDT.Domain.Factory;
using Microsoft.Extensions.Configuration;

namespace ChildHDT.Testing.HDT01
{
    [TestClass()]
    public class MessagingTests
    {

        private FactoryChild factoryChild = new FactoryChild();
        private IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

        [TestMethod()]
        public async Task SubscribeTestAsync()
        {
            // ARRANGE

            var messaging = new Messaging();
            var publisher = factoryChild.CreateChildVictim(name: "Publisher", surname: "Test", age: 10, classroom: "1ºA");

            // ACT
            await messaging.Subscribe(publisher.Id, "test/topic");

            // ASSERT

        }

        [TestMethod()]
        public async Task PublishTestAsync()
        {
            // ARRANGE

            var messaging = new Messaging();
            var publisher = factoryChild.CreateChildVictim(name: "Publisher", surname: "Test", age: 10, classroom: "1ºA");

            // ACT
            await messaging.Publish(publisher.Id, "test/topic", "TEST MESSAGE");

            // ASSERT

        }

        [TestMethod()]
        public async Task SendHelpMessage()
        {
            // ARRANGE

            var publisher = factoryChild.CreateChildVictim(name: "Publisher", surname: "Test", age: 10, classroom: "1ºA");
            var nh = new NotificationHandler();

            // ACT
            publisher.StressLevelShotUp(nh);
            // ASSERT

        }
    }
}