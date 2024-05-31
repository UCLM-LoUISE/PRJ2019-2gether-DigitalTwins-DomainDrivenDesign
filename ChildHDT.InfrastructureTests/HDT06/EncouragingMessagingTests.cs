using ChildHDT.API.ApplicationServices;
using ChildHDT.Domain.DomainServices;
using ChildHDT.Domain.Factory;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHDT.Testing.HDT06
{
    [TestClass()]
    public class EncouragingMessagingTests
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
        public void EncouragingMessage_ShouldPublishCorrectMessage()
        {
            // Arrange
            var child = factoryChild.CreateChildToMObserver("Ron", "Weasley", 11, "Potions");
            var expectedMessage = "If your peer is being bullied, don't stay silent. Your bravery can make a difference.";

            // Act
            child.StressLevelShotUp(_notificationHandler);

            // Assert
            _mockMessaging.Verify(m => m.Publish(child.Id, expectedMessage), Times.Once);
        }
    }
}
