using ChildHDT.Domain.Factory;
using ChildHDT.Domain.ValueObjects;
using ChildHDT.Infrastructure.EventSourcing.Registries;
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

        [TestMethod()]
        public async Task SpeedEventStoreTest()
        {
            // ARRANGE
            var child = factoryChild.CreateChildVictim(name: "Peter", surname: "Parker", age: 10, classroom: "4ºB");
            SpeedRegistry speedRegistry = new SpeedRegistry(child);

            // ACT
            await speedRegistry.Start();
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
            SpeedRegistry speedRegistry = new SpeedRegistry(child);

            // ACT
            await speedRegistry.Start();
            await Task.Delay(TimeSpan.FromSeconds(7));
            var events = speedRegistry.GetEvents();

            // ASSERT

            var expected = new SpeedMS(55);
            var actual = events[0].Speed;
            Assert.AreEqual(actual, expected);

        }

        [TestMethod()]
        public async Task LocationEventStoreTest()
        {
            // ARRANGE
            var child = factoryChild.CreateChildVictim(name: "Peter", surname: "Parker", age: 10, classroom: "4ºB");
            SpeedRegistry speedRegistry = new SpeedRegistry(child);

            // ACT
            await speedRegistry.Start();
            await Task.Delay(TimeSpan.FromSeconds(7));
            var events = speedRegistry.GetEvents();

            // ASSERT

            var expected = new SpeedMS(55);
            var actual = events[0].Speed;
            Assert.AreEqual(actual, expected);

        }

        [TestMethod()]
        public async Task OrientationEventStoreTest()
        {
            // ARRANGE
            var child = factoryChild.CreateChildVictim(name: "Peter", surname: "Parker", age: 10, classroom: "4ºB");
            SpeedRegistry speedRegistry = new SpeedRegistry(child);

            // ACT
            await speedRegistry.Start();
            await Task.Delay(TimeSpan.FromSeconds(7));
            var events = speedRegistry.GetEvents();

            // ASSERT

            var expected = new SpeedMS(55);
            var actual = events[0].Speed;
            Assert.AreEqual(actual, expected);

        }
    }
}
