using ChildHDT.Domain.Entities;
using ChildHDT.Infrastructure.InfrastructureServices;

namespace ChildHDT.API.ApplicationServices
{
    public class NotificationHandler
    {
        // ATTRIBUTES
        private Messaging messaging = new Messaging();
        
        // METHODS
        public void SendHelpMessage(Child child) 
        {
            var message = "" + child.Name + " " + child.Surname + " could be suffering bullying at this moment. We advice you to take a look.";
            messaging.Publish(child.Id, "help", message);
        }
    }
}
