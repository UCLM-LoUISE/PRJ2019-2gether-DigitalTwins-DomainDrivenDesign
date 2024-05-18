using ChildHDT.Domain.DomainServices;
using ChildHDT.Domain.Entities;

namespace ChildHDT.Domain.ValueObjects
{
    public class NonToMObserver : Observer
    {
        // METHODS
        public override bool IsNonToMObserver()
        {
            return true;
        }
        public override void ManageStressLevelShotUp(INotificationHandler nh, Child child)
        { 
        }
    }
}
