using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChildHDT.Domain.Entities;
using ChildHDT.Domain.ValueObjects;

namespace ChildHDT.Domain.Factory
{
    public class FactoryChild
    {
        public Child CreateChild(string name, string surname, int age, string classroom, Role role)
        {
            var child = new Child(name, surname, age, classroom);
            child.AssignRole(role);
            return child;
        }

        public Child CreateChildVictim(string name, string surname, int age, string classroom)
        {
            return CreateChild(name, surname, age, classroom, new Victim());
        }

        public Child CreateChildBully(string name, string surname, int age, string classroom)
        {
            return CreateChild(name, surname, age, classroom, new Bully());
        }

        public Child CreateChildToMObserver(string name, string surname, int age, string classroom)
        {
            return CreateChild(name, surname, age, classroom, new ToMObserver());
        }

        public Child CreateChildNonToMObserver(string name, string surname, int age, string classroom)
        {
            return CreateChild(name, surname, age, classroom, new NonToMObserver());
        }
    }
}
