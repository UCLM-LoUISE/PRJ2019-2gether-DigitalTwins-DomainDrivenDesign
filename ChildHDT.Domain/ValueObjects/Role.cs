using ChildHDT.Domain.DomainServices;
using ChildHDT.Domain.Entities;

namespace ChildHDT.Domain.ValueObjects
{
    public abstract class Role
    {
        public abstract void ManageStressLevelShotUp(INotificationHandler nh, Child child);
    }
}
