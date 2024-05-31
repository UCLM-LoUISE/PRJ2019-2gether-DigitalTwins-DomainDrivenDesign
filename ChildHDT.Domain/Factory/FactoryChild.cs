using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChildHDT.Domain.Entities;
using ChildHDT.Domain.ValueObjects;

namespace ChildHDT.Domain.Factory
{
    public class FactoryChild
    {
        public Child CreateChild(string name, string surname, int age, string classroom)
        {
            var child = new Child(name, surname, age, classroom);
            return new Child(name, surname, age, classroom);
        }

        public Child CreateChildVictim(string name, string surname, int age, string classroom)
        {
            var child = CreateChild(name, surname, age, classroom);
            child.AssignRole(new Victim());
            return child;
        }

        public Child CreateChildBully(string name, string surname, int age, string classroom)
        {
            var child = CreateChild(name, surname, age, classroom);
            child.AssignRole(new Bully());
            return child;
        }

        public Child CreateChildToMObserver(string name, string surname, int age, string classroom)
        {
            var child = CreateChild(name, surname, age, classroom);
            child.AssignRole(new ToMObserver());
            return child;
        }

        public Child CreateChildNonToMObserver(string name, string surname, int age, string classroom)
        {
            var child = CreateChild(name, surname, age, classroom);
            child.AssignRole(new NonToMObserver());
            return child;
        }
    }
}
