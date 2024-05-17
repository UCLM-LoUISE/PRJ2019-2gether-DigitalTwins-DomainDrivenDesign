using ChildHDT.Domain.DomainServices;
using ChildHDT.Domain.Entities;

namespace ChildHDT.Domain.ValueObjects
{
    public class Bully : Role
    {
        public override void ManageStressLevelShotUp(INotificationHandler nh, Child child)
        {
            // In this case, a message is sent to the bully telling him to stop
        }
    }
}