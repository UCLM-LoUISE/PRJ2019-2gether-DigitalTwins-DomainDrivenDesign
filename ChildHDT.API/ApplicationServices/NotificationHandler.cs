using ChildHDT.Domain.DomainServices;
using ChildHDT.Domain.Entities;
using ChildHDT.Infrastructure.InfrastructureServices;
using System.Threading.Tasks;

namespace ChildHDT.API.ApplicationServices
{
    public class NotificationHandler : INotificationHandler
    {
        // ATTRIBUTES
        private Messaging messaging = new Messaging();
        
        // METHODS
        public void SendHelpMessage(Child child) 
        {
            var message = "" + child.Name + " " + child.Surname + " could be suffering bullying at this moment. We advice you to take a look.";
            messaging.Publish(child.Id, "help", message);
        }

        public void AdviceMessage(Child child)
        {
            var message = "Remember: Treating others with respect is crucial. Bullying hurts. If you need to talk, we are here to help you.";
            messaging.Publish(child.Id, message);
        }

        public void EncouragingMessage(Child child)
        {
            var message = "If your peer is being bullied, don't stay silent. Your bravery can make a difference.";
            messaging.Publish(child.Id, message);
        }

        public void StressManagementMessage(Child child)
        {
            var message = "You seem to be a bit stressed. Take a break or ask for help so that you can relax!";
            messaging.Publish(child.Id, message);
        }
    } 
}
