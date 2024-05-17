using ChildHDT.Domain.DomainServices;
using ChildHDT.Domain.Entities;

namespace ChildHDT.Domain.ValueObjects
{
    public class NonToMObserver : Observer
    {
        public override void ManageStressLevelShotUp(INotificationHandler nh, Child child)
        { 
        }
    }
}
