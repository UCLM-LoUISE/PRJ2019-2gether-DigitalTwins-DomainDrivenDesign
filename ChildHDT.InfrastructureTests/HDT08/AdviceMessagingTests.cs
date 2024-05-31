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

namespace ChildHDT.Testing.HDT08
{
    [TestClass()]
    public class AdviceMessagingTests
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
        public void AdviceMessage_ShouldPublishCorrectMessage()
        {
            // Arrange
            var child = factoryChild.CreateChildBully("Draco", "Malfoy", 11, "Potions");
            var expectedMessage = "Remember: Treating others with respect is crucial. Bullying hurts. If you need to talk, we are here to help you.";

            // Act
            child.StressLevelShotUp(_notificationHandler);

            // Assert
            _mockMessaging.Verify(m => m.Publish(child.Id, expectedMessage), Times.Once);
        }
    }
}
