using ChildHDT.Domain.DomainServices;
using ChildHDT.Domain.Entities;

namespace ChildHDT.Domain.ValueObjects
{
    public class Bully : Role
    {
        public override bool IsBully()
        {
            return true;
        }

        public override void ManageStressLevelShotUp(INotificationHandler nh, Child child)
        {
            nh.AdviceMessage(child);
        }
    }
}