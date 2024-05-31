using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChildHDT.Infrastructure.InfrastructureServices;
using Moq;
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
using Microsoft.EntityFrameworkCore.InMemory.Internal;
using ChildHDT.Domain.Entities;
using ChildHDT.Domain.DomainServices;

namespace ChildHDT.Testing.HDT01
{
    [TestClass()]
    public class MessagingTests
    {
        private FactoryChild factoryChild = new FactoryChild();
        private Mock<IConfiguration> _mockConfiguration;
        private Mock<IMessaging> _mockMessaging;
        private NotificationHandler _notificationHandler;

        [TestInitialize]
        public void Setup()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockMessaging = new Mock<IMessaging>();

            _notificationHandler = new NotificationHandler(_mockConfiguration.Object, _mockMessaging.Object);
        }

        [TestMethod]
        public void SendHelpMessage_ShouldPublishCorrectMessage()
        {
            // Arrange
            var child = factoryChild.CreateChildVictim("Harry", "Potter", 11, "Potions");
            var expectedMessage = "Harry Potter could be suffering bullying at this moment. We advice you to take a look.";

            // Act
            child.StressLevelShotUp(_notificationHandler);

            // Assert
            _mockMessaging.Verify(m => m.Publish(child.Id, "help", expectedMessage), Times.Once);
        }
    }
}