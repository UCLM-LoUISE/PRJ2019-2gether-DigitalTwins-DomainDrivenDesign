using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using ChildHDT.Infrastructure.InfrastructureServices.Models;

namespace ChildHDT.Infrastructure.InfrastructureServices.Mapping
{
    public class ChildMap : ClassMap<Child>
    {
        public ChildMap() 
        {
            Table("Child");
            Id(c  => c.Id);
            Map(c => c.Name);
            Map(c => c.Surname);
            Map(c => c.Age);
            Map(c => c.Classroom);
            Map(c => c.Role);
        }
    }
}
