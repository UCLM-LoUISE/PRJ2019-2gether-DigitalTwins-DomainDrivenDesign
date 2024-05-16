﻿using ChildHDT.Domain.ValueObjects;

namespace ChildHDT.Infrastructure.InfrastructureServices.Models
{
    public class Child
    {
        // ATTRIBUTES
        public Guid Id { get; init; } //Child ID is automatically created
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Classroom { get; set; }
        public IFeatures Features { get; set; }
        public Role Role { get; set; }
        
        // METHODS

        public Child(string name, string surname, int age, string classroom) { 
            //Por factorizar
            Name = name;
            Surname = surname;
            Age = age;
            Classroom = classroom;
        }

   
        public void StressLevelShotUp()
        {
            this.Role.ManageStressLevelShotUp();
        }

        public void AssignRole(Role role)
        { 
            this.Role = role; 
        }

    }
}