using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHDT.Infrastructure.InfrastructureServices.Context
{
    public interface IUnitOfwork
    {
        DbContext Context { get; }
        public Task SaveChangesAsync();
    }
}
