using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChildHDT.Domain.Entities;
using ChildHDT.Infrastructure.InfrastructureServices.Helper;
using NHibernate;

namespace ChildHDT.Infrastructure.InfrastructureServices
{
    public class RepositoryChild
    {
        private ISession _session = NHibernateHelper.GetSession();

        public void Add(Child child)
        {
            if (child == null)
                throw new ArgumentNullException("Child is null");

            _session.Save(child);
        }

        public void Remove(Child child)
        {
            if (child == null)
                throw new ArgumentNullException("Child is null");

            _session.Delete(child);
        }

        public void Update(Child child)
        {
            if (child == null)
                throw new ArgumentNullException("Child is null");

            _session.Update(child);
        }

        public Child FindById(Guid id)
        {
            return _session.Get<Child>(id);
        }
    }
}
