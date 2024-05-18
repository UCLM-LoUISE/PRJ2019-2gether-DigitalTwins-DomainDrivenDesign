using ChildHDT.Domain.DomainServices;
using ChildHDT.Domain.Entities;

namespace ChildHDT.Domain.ValueObjects
{
    public class ToMObserver : Observer
    {
        // METHODS
        public override bool IsToMObserver()
        {
            return true;
        }
        public override void ManageStressLevelShotUp(INotificationHandler nh, Child child)
        {
            // A message is sent to encourage them to act
        }
    }
}
