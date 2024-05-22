using ChildHDT.Domain.Factory;
using ChildHDT.Domain.ValueObjects;
using ChildHDT.Infrastructure.EventSourcing.Registries;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHDT.Testing.HDT05
{
    [TestClass()]
    public class StressCalculationTests
    {
        private FactoryChild factoryChild = new FactoryChild();
        private IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

        [TestMethod()]
        public async Task SpeedEventStoreTest()
        {
            // ARRANGE
            var child = factoryChild.CreateChildVictim(name: "Peter", surname: "Parker", age: 10, classroom: "4ºB");
            SpeedRegistry speedRegistry = new SpeedRegistry(child, configuration);

            // ACT
            await Task.Delay(TimeSpan.FromSeconds(7));
            var events = speedRegistry.GetEvents();

            // ASSERT

            var expected = new SpeedMS(55);
            var actual = events[0].Speed;
            Assert.AreEqual(actual, expected);
            
        }

        [TestMethod()]
        public async Task StressEventStoreTest()
        {
            // ARRANGE
            var child = factoryChild.CreateChildVictim(name: "Peter", surname: "Parker", age: 10, classroom: "4ºB");
            StressRegistry stressRegistry = new StressRegistry(child, configuration);

            // ACT
            await Task.Delay(TimeSpan.FromSeconds(7));
            var events = stressRegistry.GetEvents();

            // ASSERT

            var expected = new Stress(value: 1, level: "High");
            var actual = events[0].Stress;
            Assert.AreEqual(actual, expected);

        }

        [TestMethod()]
        public async Task LocationEventStoreTest()
        {
            // ARRANGE
            var child = factoryChild.CreateChildVictim(name: "Peter", surname: "Parker", age: 10, classroom: "4ºB");
            LocationRegistry locationRegistry = new LocationRegistry(child, configuration);

            // ACT
            await Task.Delay(TimeSpan.FromSeconds(7));
            var events = locationRegistry.GetEvents();

            // ASSERT

            var expected = new Location(latitude: 55, longitude: 30);
            var actual = events[0].Location;
            Assert.AreEqual(actual, expected);

        }

        [TestMethod()]
        public async Task OrientationEventStoreTest()
        {
            // ARRANGE
            var child = factoryChild.CreateChildVictim(name: "Peter", surname: "Parker", age: 10, classroom: "4ºB");
            OrientationRegistry orientationRegistry = new OrientationRegistry(child, configuration);

            // ACT
            await Task.Delay(TimeSpan.FromSeconds(7));
            var events = orientationRegistry.GetEvents();

            // ASSERT

            var expected = new Orientation(20);
            var actual = events[0].Orientation;
            Assert.AreEqual(actual, expected);

        }
    }
}
