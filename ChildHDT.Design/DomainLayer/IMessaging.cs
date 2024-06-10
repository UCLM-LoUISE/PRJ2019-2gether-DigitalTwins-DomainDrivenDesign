using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHDT.Domain.DomainServices
{
    public interface IMessaging
    {
        public Task Publish(Guid childId, string messageType, string message);
        public Task Publish(Guid childId, string message);
    }
}
