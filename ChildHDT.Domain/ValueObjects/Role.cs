using ChildHDT.Domain.DomainServices;
using ChildHDT.Domain.Entities;

namespace ChildHDT.Domain.ValueObjects
{
    public abstract class Role
    {
        public abstract void ManageStressLevelShotUp(INotificationHandler nh, Child child);
        public virtual bool IsVictim() {  return false; }
        public virtual bool IsBully() { return false; }
        public virtual bool IsToMObserver() { return false; }
        public virtual bool IsNonToMObserver() { return false; }

    }
}
