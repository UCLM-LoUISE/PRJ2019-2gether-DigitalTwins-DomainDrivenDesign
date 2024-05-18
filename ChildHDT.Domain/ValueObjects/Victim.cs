using ChildHDT.Domain.DomainServices;
using ChildHDT.Domain.Entities;

namespace ChildHDT.Domain.ValueObjects
{
    public class Victim : Role
    {
        public override bool IsVictim()
        {
            return true;
        }

        public override void ManageStressLevelShotUp(INotificationHandler nh, Child child)
        {
            nh.SendHelpMessage(child);
        }
    }
}
