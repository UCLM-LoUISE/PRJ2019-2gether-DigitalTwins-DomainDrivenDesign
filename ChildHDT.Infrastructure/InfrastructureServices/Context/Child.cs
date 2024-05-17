using ChildHDT.Domain.ValueObjects;


namespace ChildHDT.Infrastructure.InfrastructureServices.Context
{
    public class Child
    {
        // ATTRIBUTES
        public Guid Id { get; init; } //Child ID is automatically created
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Classroom { get; set; }

        

    }
}