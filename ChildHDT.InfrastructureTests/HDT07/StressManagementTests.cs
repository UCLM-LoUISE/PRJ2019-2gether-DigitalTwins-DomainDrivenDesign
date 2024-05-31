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

namespace ChildHDT.Testing.HDT07
{
    [TestClass()]
    public class StressManagementTests
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
        public void ManageStressMessage_ShouldPublishCorrectMessage()
        {
            // Arrange
            var child = factoryChild.CreateChildVictim("Harry", "Potter", 11, "Potions");
            var expectedMessage = "You seem to be a bit stressed. Take a break or ask for help so that you can relax!";

            // Act
            child.StressLevelShotUp(_notificationHandler);

            // Assert
            _mockMessaging.Verify(m => m.Publish(child.Id, expectedMessage), Times.Once);
        }
    }
}
