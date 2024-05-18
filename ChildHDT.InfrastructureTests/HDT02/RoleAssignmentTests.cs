using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using ChildHDT.API.ApplicationServices;
using ChildHDT.Domain.Entities;
using ChildHDT.Domain.Factory;
using ChildHDT.Domain.ValueObjects;
using ChildHDT.Infrastructure.InfrastructureServices;
using ChildHDT.Infrastructure.InfrastructureServices.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChildHDT.Testing.HDT02
{
    [TestClass]
    public class RoleAssignmentTests
    {
        private FactoryChild factoryChild = new FactoryChild();
        [TestMethod]
        public void AssignRole()
        {
            // ARRANGE

            var childVictim = factoryChild.CreateChild(name: "John", surname: "Doe", age: 10, classroom: "4ºB");
            var childBully = factoryChild.CreateChild(name: "John", surname: "Doe", age: 10, classroom: "4ºB");
            var childToMObserver = factoryChild.CreateChild(name: "John", surname: "Doe", age: 10, classroom: "4ºB");
            var childNonToMObserver = factoryChild.CreateChild(name: "John", surname: "Doe", age: 10, classroom: "4ºB");

            // ACT 

            childVictim.AssignRole(new Victim());
            childBully.AssignRole(new Bully());
            childToMObserver.AssignRole(new ToMObserver());
            childNonToMObserver.AssignRole(new NonToMObserver());

            // ASSERT

            Assert.IsTrue(childVictim.IsVictim(), "ChildVictim should be assigned the Victim role.");
            Assert.IsTrue(childBully.IsBully(), "ChildBully should be assigned the Bully role.");
            Assert.IsTrue(childToMObserver.IsToMObserver(), "ChildToMObserver should be assigned the ToMObserver role.");
            Assert.IsTrue(childNonToMObserver.IsNonToMObserver(), "ChildNonToMObserver should be assigned the NonToMObserver role.");
        }

        [TestMethod]
        public void AssignRoleService()
        {
            // ARRANGE

            var child = factoryChild.CreateChildVictim(name: "John", surname: "Doe", age: 10, classroom: "4ºB");
            var repo = new RepositoryChild(new ChildContext());
            repo.Add(child);
            repo.Save();
            var roleService = new RoleAssignment(repo);

            // ACT

            roleService.AssignRoleBullyToChild(child.Id);
            repo.Save();

            // ASSERT

            var after = repo.FindById(child.Id);
            Assert.IsTrue(after.IsBully(), "ChildBully should be assigned the Bully role.");

        }
    }
}
