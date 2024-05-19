using ChildHDT.API.ApplicationServices;
using ChildHDT.Domain.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        [TestMethod()]
        public void AdviceMessageTest()
        {
            // ARRANGE

            var publisher = factoryChild.CreateChildBully(name: "Publisher", surname: "Test", age: 10, classroom: "1ºA");
            var nh = new NotificationHandler();

            // ACT
            publisher.StressLevelShotUp(nh);
            // ASSERT
        }
    }
}
