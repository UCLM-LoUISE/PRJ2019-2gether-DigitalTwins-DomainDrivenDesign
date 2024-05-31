using ChildHDT.Domain.Factory;
using ChildHDT.Domain.ValueObjects;
using ChildHDT.Infrastructure.EventSourcing.Events;
using ChildHDT.Infrastructure.EventSourcing.Registries;
using ChildHDT.Infrastructure.IntegrationServices;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MQTTnet.Client;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using MQTTnet.Packets;

namespace ChildHDT.Testing.HDT05
{
    [TestClass()]
    public class StressCalculationTests
    {
        private FactoryChild factoryChild = new FactoryChild();
        private Mock<IMqttClient> _mockMqttClient;
        private SpeedRegistry _speedRegistry;

        [TestInitialize]
        public void Setup()
        {
            _mockMqttClient = new Mock<IMqttClient>();

            _mockMqttClient.Setup(client => client.ConnectAsync(It.IsAny<MqttClientOptions>(), default)).ReturnsAsync(new MqttClientConnectResult());
            
            var subscribeResult = new MqttClientSubscribeResult(1, new List<MqttClientSubscribeResultItem>
            {
                new MqttClientSubscribeResultItem(new MqttTopicFilter(), MqttClientSubscribeResultCode.GrantedQoS1)
            }, null, new List<MqttUserProperty>());

            _mockMqttClient.Setup(client => client.SubscribeAsync(It.IsAny<MqttClientSubscribeOptions>(), default))
                .ReturnsAsync(subscribeResult);

            _speedRegistry = new SpeedRegistry(Guid.NewGuid(), "localhost", 1883, "user", "password");

            var clientField = typeof(EventStore<SpeedEvent>).GetField("_client", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            clientField.SetValue(_speedRegistry, _mockMqttClient.Object);
        }

        [TestMethod]
        public void SpeedEventStoreTest()
        {
            // ARRANGE
            var child = factoryChild.CreateChildVictim("Peter", "Parker", 10, "4ºB");
            SpeedRegistry speedRegistry = (child.Features as PWAFeatures).SpeedRegistry;

            // Simulating an event reception
            var speedEvent = new SpeedEvent(new SpeedMS(55), DateTime.Now);
            var payload = JsonSerializer.Serialize(speedEvent.Speed);
            var applicationMessage = new MqttApplicationMessageBuilder()
                .WithPayload(payload)
                .Build();

            var publishPacket = new MqttPublishPacket();
            var acknowledgeHandler = new Mock<Func<MqttApplicationMessageReceivedEventArgs, CancellationToken, Task>>().Object;
            var eventArgs = new MqttApplicationMessageReceivedEventArgs(
                clientId: "testClientId",
                applicationMessage: applicationMessage,
                publishPacket: publishPacket,
                acknowledgeHandler: acknowledgeHandler
            );

            _mockMqttClient.Raise(client => client.ApplicationMessageReceivedAsync += null, eventArgs);
            // ACT
            var events = speedRegistry.GetEvents();

            // ASSERT
            var expected = new SpeedMS(55);
            var actual = events[0].Speed;
            Assert.AreEqual(expected, actual);
        }

        //[TestMethod()]
        //public async Task StressEventStoreTest()
        //{
        //    // ARRANGE
        //    var child = factoryChild.CreateChildVictim(name: "Peter", surname: "Parker", age: 10, classroom: "4ºB");
        //    StressRegistry stressRegistry = new StressRegistry(child.Id);

        //    // ACT
        //    await Task.Delay(TimeSpan.FromSeconds(7));
        //    var events = stressRegistry.GetEvents();

        //    // ASSERT
        //    var expected = new Stress(value: 1, level: "High");
        //    var actual = events[0].Stress;
        //    Assert.AreEqual(actual, expected);
        //}

        //[TestMethod()]
        //public async Task LocationEventStoreTest()
        //{
        //    // ARRANGE
        //    var child = factoryChild.CreateChildVictim(name: "Peter", surname: "Parker", age: 10, classroom: "4ºB");
        //    LocationRegistry locationRegistry = new LocationRegistry(child.Id);

        //    // ACT
        //    await Task.Delay(TimeSpan.FromSeconds(7));
        //    var events = locationRegistry.GetEvents();

        //    // ASSERT
        //    var expected = new Location(latitude: 55, longitude: 30);
        //    var actual = events[0].Location;
        //    Assert.AreEqual(actual, expected);

        //}

        //[TestMethod()]
        //public async Task OrientationEventStoreTest()
        //{
        //    // ARRANGE
        //    var child = factoryChild.CreateChildVictim(name: "Peter", surname: "Parker", age: 10, classroom: "4ºB");
        //    OrientationRegistry orientationRegistry = new OrientationRegistry(child.Id);

        //    // ACT
        //    await Task.Delay(TimeSpan.FromSeconds(7));
        //    var events = orientationRegistry.GetEvents();

        //    // ASSERT
        //    var expected = new Orientation(20);
        //    var actual = events[0].Orientation;
        //    Assert.AreEqual(actual, expected);

        //}
    }
}
