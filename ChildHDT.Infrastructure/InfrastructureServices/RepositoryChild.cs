using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChildHDT.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace ChildHDT.Infrastructure.InfrastructureServices
{
    public class RepositoryChild
    {
        private readonly DbContext _context;

        public RepositoryChild(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Child> GetAll()
        {
            return _context.Set<Child>().ToList();
        }

        public Child FindById(int id)
        {
            return _context.Set<Child>().Find(id);
        }

        public void Add(Child child)
        {
            _context.Set<Child>().Add(child);
        }

        public void Update(Child child)
        {
            _context.Entry(child).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var child = FindById(id);
            if (child != null)
            {
                _context.Set<Child>().Remove(child);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
