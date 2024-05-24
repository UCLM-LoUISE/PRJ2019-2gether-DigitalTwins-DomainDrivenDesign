using ChildHDT.Domain.DomainServices;
using ChildHDT.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ChildHDT.Domain.Entities
{
    public class Child
    {
        // ATTRIBUTES
        public Guid Id { get; private set; } 
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Classroom { get; set; }
        public Role Role { get; set; }
        [NotMapped]
        [JsonIgnore]
        public IFeatures? Features { get; set; }
        
        // METHODS

        public Child(string name, string surname, int age, string classroom) { 
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Age = age;
            Classroom = classroom;
        }

        public bool IsVictim()
        {
            return Role.IsVictim();
        }
        public bool IsBully()
        {
            return Role.IsBully();
        }
        public bool IsToMObserver()
        {
            return Role.IsToMObserver();
        }
        public bool IsNonToMObserver()
        {
            return Role.IsNonToMObserver();
        }


        public void StressLevelShotUp(INotificationHandler nh)
        {
            this.Role.ManageStressLevelShotUp(nh, this);
        }

        public void AssignRole(Role role)
        { 
            this.Role = role; 
        }

    }
}